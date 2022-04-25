using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] private float movementSpeed; 
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;
    [SerializeField] private float velPower;
    [SerializeField] private float frictionAmount;
    [Space]


    [Header("Jumping")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCoyoteTime;
    [SerializeField] private float jumpBufferTime;
    [SerializeField] private float jumpCutMultiplier;
    [SerializeField] private float fallGravityMultiplier;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask groundLayer;
    //[SerializeField] private int groundLayerID;
    [Space(3)]

    [SerializeField] private Transform companionTarget;

    private float gravityScale;


    //state variables
    private bool isJumping;
    private bool jumpInputReleased;
    public bool isFacingLeft;

    //timers
    private float lastGroundTime;
    private float lastJumpTime;
    private float lastPressedJumpTime;




    //get horizontal input value (a/d or left/right)
    private float horizontalInput;
    private bool jump = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;
    }

    void Update()
    {
        //update timers
        UpdateTimers();
        //set horizontal input to current input
        horizontalInput = Input.GetAxis("Horizontal");
        //check for/handle inputs
        InputHandler();
        GroundCheck();
        
        AnimationHandler();

    }



    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void UpdateTimers()
    {
        lastGroundTime -= Time.deltaTime;
        lastJumpTime -= Time.deltaTime;
    }


    private void MovePlayer()
    {
        //calculate direction and speed of movement
        float targetSpeed = horizontalInput * movementSpeed;
        //calculate difference between current velocity and desired velocity
        float speedDif = targetSpeed - rb.velocity.x;
        //change acceleration/decceleration
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        //apply movement
        rb.AddForce(movement * Vector2.right);

        if (lastGroundTime > 0 && Mathf.Abs(horizontalInput) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
            amount *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }

        if (lastGroundTime > 0 && lastJumpTime > 0 && !isJumping)
        {
            Jump();
        }

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = gravityScale * fallGravityMultiplier;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
    }

    
    private void GroundCheck()
    {
        //return true if grounded
        if (Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer))
        {
            lastGroundTime = jumpCoyoteTime;
            isJumping = false;
        }
    }


    private void Jump()
    {
        //apply force in impulse
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        //reset timers/set state variables
        lastGroundTime = 0;
        lastJumpTime = 0;
        isJumping = true;
        jumpInputReleased = false;
    }

    private bool CanJump()
    {
        return lastGroundTime > 0 && !isJumping;
    }


    private void InputHandler()
    {
        if (Input.GetButtonDown("Jump"))
        {
            lastJumpTime = jumpBufferTime;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            //implement jump cut
            if (rb.velocity.y > 0 && isJumping)
            {
                rb.AddForce(Vector2.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
            }
            jumpInputReleased = true;
            lastJumpTime = 0;
        }
    }


    private void AnimationHandler()
    {
        if (horizontalInput > 0.01f)
        {
            isFacingLeft = false;
        }
        else if (horizontalInput < -0.01f)
        {
            isFacingLeft = true;
        }
    }

}
