using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerJoystick : MonoBehaviour
{
    public float speed;
    public FixedJoystick fixedJoystick;
    public Rigidbody2D rb;
    public bool isGrounded;
    public float jumpForce = 10;
    Animator animator;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if ( fixedJoystick.Vertical>0 && isGrounded)
        {
            rb.velocity= new Vector2 (rb.velocity.x,jumpForce);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);


        }
        FlipSprite();
     }
    private void FlipSprite() {
        if (fixedJoystick.Horizontal > 0)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }
        else if (fixedJoystick.Horizontal < 0)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
        }
    }
    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        animator.SetFloat("XVelocity",Math.Abs( rb.velocity.x));
        animator.SetFloat("YVelocity",  rb.velocity.y);

    }

    private void OnCollisionEnter2D(Collision2D other) {
        isGrounded = true;
        animator.SetBool("isJumping", !isGrounded);

    }
}
