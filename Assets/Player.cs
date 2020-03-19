﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 1.0f;
    [SerializeField] float firstJumpSpeed = 25.0f;
    [SerializeField] float secondJumpSpeed = 10.0f;
    [SerializeField] float climbSpeed = 3.0f;


    public Rigidbody2D rigidbody;
    private Animator animatorComponent;
    int jumpcount = 0;
    private SpriteRenderer spriteComponent;
    bool running = false;
    Collider2D collider2D;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animatorComponent = GetComponent<Animator>();
        spriteComponent = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Run();
        HandleHorizontalMovement();
        Jump();
        ClimbLadder();
    }

    void Run()
    {

        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, rigidbody.velocity.y);
        rigidbody.velocity = playerVelocity;

    }

    private void ClimbLadder()
    {
        if (!collider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
            return;
        }
        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(rigidbody.velocity.x,controlThrow*climbSpeed);
        rigidbody.velocity = climbVelocity;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && jumpcount != 2 || Input.GetKeyDown(KeyCode.UpArrow) && jumpcount != 2)
        {
            if(jumpcount == 0)
            {
                rigidbody.AddForce(new Vector2(0, firstJumpSpeed), ForceMode2D.Impulse);
                jumpcount++;
            }
            else
            {
                rigidbody.AddForce(new Vector2(0, secondJumpSpeed), ForceMode2D.Impulse);
                jumpcount++;
            }
            //Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            //rigidbody.velocity += jumpVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpcount = 0;
    }

    private void HandleHorizontalMovement()
    {
        var direction = Input.GetAxisRaw("Horizontal");
        var velocity = rigidbody.velocity;
        
        if (direction > 0f)
        {
            spriteComponent.flipX = false; // Faces right
        }
        else if (direction < 0f)
        {
            spriteComponent.flipX = true; // Faces left
        }


        var position = transform.position;

        animatorComponent.SetBool(10, Mathf.Abs(direction) > 0.0f);
    }
}
