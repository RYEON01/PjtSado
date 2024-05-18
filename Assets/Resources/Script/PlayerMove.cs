using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed, slopeForce, raycastLength;
    private Rigidbody2D rigid;
    private bool isGrounded;

    void Awake() => rigid = GetComponent<Rigidbody2D>();

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2((isGrounded && h == 0) ? 0f : h * maxSpeed, rigid.velocity.y);
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        // Cast the raycast in the direction the player is moving
        Vector2 raycastDirection = new Vector2(h, -1).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, raycastLength);
        isGrounded = hit.collider != null;

        if(isGrounded && Vector2.Angle(hit.normal, Vector2.up) <= 45)
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