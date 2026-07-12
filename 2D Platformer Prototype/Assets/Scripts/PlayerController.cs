using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("DoubleJump")]
    [SerializeField] private float doubleJumpForce;

    public bool canDoubleJump;

    [Header("Colision Info")]
    [SerializeField] private float groundCheck;
    private bool isGrounded;
    [SerializeField] private LayerMask WhatIsGround;
    private bool isAirborne;

    // direção que o personagem está virado: 1 = direita, -1 = esquerda
    private int facingDirection = 1;
    // flag lógica — true se o personagem estiver voltado para a direita
    private bool facingRight = true;

    private float xInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    
    private void Start()
    {
        
    }
 
    private void Update()
    {
        UpdateAirborneStatus();

        HandleCollision();
        HandleInput();
        HandleMovement();
        HandleFlip();
        HandleAnimations();

    }

    private void UpdateAirborneStatus()
    {
        if (isGrounded && isAirborne) HandleLanding();
        if (!isGrounded && !isAirborne) BecomeAirborne();
    }

    private void BecomeAirborne()
    {
        isAirborne = true;
    }

    private void HandleLanding()
    {
        isAirborne = false;
        canDoubleJump = true;
    }

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space)) JumpButton();

    }

    private void JumpButton()
    {
        if (isGrounded)
        {
            Jump();
        }else if (canDoubleJump)
        {
            DoubleJump();
            canDoubleJump = false;
        }


    }

    private void Jump() => rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    private void DoubleJump() => rb.linearVelocity = new Vector2(rb.linearVelocity.x, doubleJumpForce);
        

    private void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheck, WhatIsGround);
    }

    private void HandleAnimations()
    {
        anim.SetFloat("xVelocity", xInput);
        //anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        anim.SetBool("isGrounded",isGrounded);
    }

    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }

    private void HandleFlip()
    {
        //if (rb.linearVelocity.x < 0 && facingRight || rb.linearVelocity.x > 0 && ! facingRight) Flip();
        if (xInput < 0 && facingRight || xInput > 0 && !facingRight) Flip();
    }

    private void Flip()
    {
        facingDirection *= -1;  // equivalente a "facingDirection = facingDirection * -1"
        //transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        float yRotation = facingRight ? 0f : 180f;
        transform.localEulerAngles = new Vector3(0f, yRotation, 0f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheck));
    }


}
