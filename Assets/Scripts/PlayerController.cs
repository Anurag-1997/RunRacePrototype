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
    private void Awake()
    {
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
            wallSlide = false;
            playerVelocity = 0;
            Jump();
        }
        else
        {
            gravity = 30f;
            playerVelocity -= gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && doubleJump)
            {
                print("Jumped!!!");
                playerVelocity += playerJumpForce * 0.5f;
                doubleJump = false;
                print("Double Jump");
            }
            
        }
        playerMove.Normalize();

        playerMove *= speed;

        playerMove.y = playerVelocity;
        characterController.Move(playerMove * Time.deltaTime);

    }

    private void Jump()
    {
        
        if(Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0))
        {
            //wallSlide = false;
            print("Jumped!!!");
            playerVelocity = playerJumpForce ;
            doubleJump = true;
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
                    print("sliding");
                    wallSlide = true;
                }
                else if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    //Jump();
                    playerVelocity = playerJumpForce;
                    doubleJump = false;
                    wallSlide = false;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180f, transform.eulerAngles.z);
                }
                
            }
        }
    }

}
