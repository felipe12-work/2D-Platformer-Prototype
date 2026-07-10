using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float moveSpeed;

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
        xInput = Input.GetAxisRaw("Horizontal");

        HandleMovement();
        HandleAnimations();

    }

    private void HandleAnimations()
    {
        anim.SetFloat("xVelocity", xInput);
        //anim.SetFloat("xVelocity", xInput rb.linearVelocity.x);
    }

    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }


}
