using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerController : MonoBehaviour
{
    CharacterController AIcharacterController;
    Vector3 AIplayerMove;
    [SerializeField] private float AIspeed; //playerSpeed
    public float AIplayerJumpForce;
    public float AIplayerVelocity = 0;
    public float AIgravity;
    private bool AIdoubleJump;
    private bool AIwallSlide;
    private bool AIplayerTurn;
    public Animator AIanim;
    public float timeDelay;
    bool AIjump = true;

    private void Awake()
    {
        AIanim = GetComponent<Animator>();
        AIcharacterController = GetComponent<CharacterController>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AIplayerMove = Vector3.zero;
        AIplayerMove = transform.forward;
        if (AIcharacterController.isGrounded)
        {
            AIwallSlide = false;
            AIplayerVelocity = 0;
            RayCastMethod();

            print("Grounded");
            AIanim.SetBool("Grounded", AIcharacterController.isGrounded);
        }
        if(!AIwallSlide)
        {
            AIanim.SetBool("WallSlide", true);
            print("wall slide off");
            AIgravity = 30f;
            AIplayerVelocity -= AIgravity * Time.deltaTime;
        }
        else
        {
            AIgravity = 10f;
            AIplayerVelocity -= AIgravity * Time.deltaTime;
        }
        AIanim.SetBool("Grounded", AIcharacterController.isGrounded);
        AIanim.SetBool("WallSlide", AIwallSlide);
        AIplayerMove.Normalize();

        AIplayerMove *= AIspeed;

        AIplayerMove.y = AIplayerVelocity;
        AIcharacterController.Move(AIplayerMove * Time.deltaTime);


    }

    private void RayCastMethod()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward,out hit,10f))
        {
            if(hit.collider.tag=="Wall")
            {
                AIJump();
            }
            Debug.DrawLine(transform.position, hit.point, Color.magenta);
            print("raycast");
        }
    }

    private void AIJump()
    {
        AIanim.SetTrigger("Jump");
        print("Jump!");
        AIplayerVelocity = AIplayerJumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.tag=="Wall")
        {
            if(AIjump)
            {
                StartCoroutine(AIJumpDelay(timeDelay));
                print("AICoroutine start");
            }
            //if (AIplayerVelocity < -0.2f)
            //{
            //    AIanim.SetBool("WallSlide", true);
            //    print("Sliding");
            //    AIwallSlide = true;
            //}
            //else /*if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))*/ // need to fix the logic according to our game progress
            //{



            //    //jump();
            //    //AIplayerVelocity = AIplayerJumpForce;
            //    //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180f, transform.eulerAngles.z);
            //    //AIdoubleJump = false;
            //    //AIwallSlide = false;

            //}

        }
    }
    IEnumerator AIJumpDelay(float timeDelay)
    {
        //AIplayerTurn = false;
        AIwallSlide = true;
        AIjump = false;
        yield return new WaitForSeconds(timeDelay);
        if(!AIcharacterController.isGrounded)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180f, transform.eulerAngles.z);
            AIplayerVelocity = AIplayerJumpForce;
            AIanim.SetTrigger("Jump");
            AIdoubleJump = false;
            AIwallSlide = false;
        }
        //AIplayerTurn = true;
        AIjump = true;
    }
}
