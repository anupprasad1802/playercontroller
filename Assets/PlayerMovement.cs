using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 400f;
    [SerializeField] float jumpForce=250f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer sprite;

    float moveX;
    bool isGrounded;

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        anim.SetBool("run", false);
        moveX = Input.GetAxisRaw("Horizontal");

        if (moveX != 0)
        {
            anim.SetBool("run",true);
            rb.velocity = new Vector2(moveX * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
        }

        Flip();
        Jump();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            anim.SetBool("jump",true);
            rb.velocity = transform.up * jumpForce * Time.fixedDeltaTime;
            isGrounded = false;
        }
    }

    void Flip()
    {
        if (moveX >= 1)
        {
            sprite.flipX = false;
        }
        if (moveX <= -1)
        {
            sprite.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            anim.SetBool("jump", false);
            isGrounded = true;
        }
    }

}
