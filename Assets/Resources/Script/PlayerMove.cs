using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float slopeForce;
    public float raycastLength;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    bool isGrounded;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if any horizontal input is detected
        float h = Input.GetAxisRaw("Horizontal");

        // If the player is grounded and no horizontal input is detected, freeze the velocity
        if(isGrounded && h == 0)
        {
            rigid.velocity = new Vector2(0f, rigid.velocity.y);
        }
        else
        {
            // Move horizontally
            rigid.velocity = new Vector2(h * maxSpeed, rigid.velocity.y);
        }

        // Flip sprite
        if(h != 0)
            spriteRenderer.flipX = h < 0;

        // Set animation
        anim.SetBool("isWalking", h != 0);
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastLength);
        isGrounded = hit.collider != null;

        if(isGrounded) {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            if(slopeAngle > 0 && slopeAngle <= 45) {
                Vector2 slopeForceDirection = Vector2.Perpendicular(hit.normal).normalized * -Mathf.Sign(hit.normal.x);
                rigid.AddForce(slopeForceDirection * slopeForce, ForceMode2D.Force);
            }
        }
    }
}
