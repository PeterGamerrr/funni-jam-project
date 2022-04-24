using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    CharacterController2D controller;

    [SerializeField] string groundTag; 
    [SerializeField] float movementSpeed; 
    [SerializeField] float jumpForce;

    //get horizontal input value (a/d or left/right)
    private float horizontalInput;
    private bool jump = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }



    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        jump = Input.GetButtonDown("Jump");
        //move the player
        controller.Move(horizontalInput * movementSpeed * Time.fixedDeltaTime, false, Jump());
        //jump = false;
    }


    private bool Jump()
    {
        //check if jump key is pressed
        if (Input.GetButtonDown("Jump"))
        {
            //if yes, return true
            return true;
        }
        //if not, return false
        return false;
    }
    

}
