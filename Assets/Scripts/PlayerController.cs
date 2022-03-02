using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    Vector3 playerMove;
    [SerializeField] private float speed; //playerSpeed
    public float playerJumpForce;
    public float playerVelocity = 0;
    public float gravity = 30f;
    private bool doubleJump;
    private bool wallSlide;
    private bool playerTurn;

    public Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

    }

    private void Start()
    {
        
    }

    private void Update()
    {
        playerMove = Vector3.zero;
        playerMove = transform.forward;
        if(characterController.isGrounded)
        {
            anim.SetBool("Grounded", characterController.isGrounded);
            wallSlide = false;
            playerVelocity = 0;
            Jump();
            if(playerTurn)
            {
                playerTurn = false;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180f, transform.eulerAngles.z);

            }
        }
        if(!wallSlide)
        {
            //anim.SetBool("WallSlide", true);
            print("Wall sliding");
            gravity = 30f;
            playerVelocity -= gravity * Time.deltaTime;
        }
        else
        {
            print("Wallsliding");
            //gravity = 15f;
            playerVelocity -= gravity * Time.deltaTime * 0.5f;
        }
        anim.SetBool("Grounded", characterController.isGrounded);
        anim.SetBool("WallSlide", wallSlide);
        //else
        //{
        //    gravity = 30f;
        //    playerVelocity -= gravity * Time.deltaTime;

        //    //THIS LOGIC IS FOR DOUBLE JUMP,WE WILL ACTIVATE IF REQUIRED

        //    //if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && doubleJump)
        //    //{
        //    //    print("Jumped!!!");
        //    //    playerVelocity += playerJumpForce * 0.5f;
        //    //    doubleJump = false;
        //    //    print("Double Jump");
        //    //}

        //}
        playerMove.Normalize();

        playerMove *= speed;

        playerMove.y = playerVelocity;
        characterController.Move(playerMove * Time.deltaTime);



    }

    private void Jump()
    {
        
        if(Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Jump");
            //wallSlide = false;
            //print("Jumped!!!");
            playerVelocity = playerJumpForce ;
            //doubleJump = true;
        }
       

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(!characterController.isGrounded)
        {
            if(hit.collider.tag=="Wall")
            {
                if (playerVelocity < 0f) 
                {
                    anim.SetBool("WallSlide", true);
                    print("sliding");
                    wallSlide = true;
                }
                else if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) //need to fix the logic according to our game progress
                {
                    //Jump();
                    playerVelocity = playerJumpForce;
                    doubleJump = false;
                    wallSlide = false;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180f, transform.eulerAngles.z);
                }
                
            }
        }
        else
        {
            if (transform.forward != hit.collider.transform.right && hit.collider.tag == "Ground" && !playerTurn) 
            {
                playerTurn = true;
                print("Player restricted from turning");
            }
        }
    }

}
