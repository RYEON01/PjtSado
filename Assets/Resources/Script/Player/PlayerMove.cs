using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed, slopeForce, raycastLength;
    private Rigidbody2D rigid;
    private bool isGrounded;
    private bool canMove = true;
    private bool canFlip = true; 
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Awake() 
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (canMove)
        {
            rigid.velocity = new Vector2((isGrounded && h == 0) ? 0f : h * maxSpeed, rigid.velocity.y);
            animator.SetBool("isWalking", h != 0);
            if (canFlip && h != 0) // Check if canFlip is true before flipping
            {
                spriteRenderer.flipX = h < 0; // Flip the sprite based on the direction of movement
            }
        }
        else
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            animator.SetBool("isWalking", false);
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            float h = Input.GetAxisRaw("Horizontal");
            // Cast the raycast in the direction the player is moving
            Vector2 raycastDirection = new Vector2(h, -1).normalized;
            int layerMask = (1 << LayerMask.NameToLayer("Ground"));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, raycastLength, layerMask);
            isGrounded = hit.collider != null;

            if (isGrounded && Vector2.Angle(hit.normal, Vector2.up) <= 45)
            {
                // Project the player's velocity onto the slope's normal vector
                Vector2 velocityPerpendicularToSlope = Vector2.Dot(rigid.velocity, hit.normal) * hit.normal;
                // Subtract this from the player's velocity to get the component of the velocity that is parallel to the slope
                Vector2 velocityParallelToSlope = rigid.velocity - velocityPerpendicularToSlope;
                // Set the player's velocity to this value
                rigid.velocity = velocityParallelToSlope;
            }

            // If there is no keydown, set the player's horizontal velocity to zero
            if (h == 0)
            {
                rigid.velocity = new Vector2(0, rigid.velocity.y);
            }
            else
            {
                rigid.velocity = new Vector2(h * maxSpeed, rigid.velocity.y);
            }
        }
    }
    
    public void SetPlayerMovement(bool canMove)
    {
        this.canMove = canMove;
        this.canFlip = canMove; // Set canFlip to the same value as canMove
        if (!canMove)
        {
            rigid.velocity = Vector2.zero;
            animator.SetBool("isWalking", false);
        }
    }
}