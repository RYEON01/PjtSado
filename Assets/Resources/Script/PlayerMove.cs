using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool isColliding; // 콜라이더가 겹쳐있는지 여부를 나타내는 변수
    bool hasLoggedOverlap = false; // Add this line
    public float maxSpeed;
    public float slopeForce;
    public float raycastLength;
    public Transform chorangTransform; // Reference to Chorang's transform
    public float chorangOffsetX; // Offset value for Chorang's x position
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    bool isGrounded;
    SpriteRenderer chorangSpriteRenderer; // Reference to Chorang's sprite renderer
    public GameObject collidedObject; // 충돌한 객체를 저장하는 변수
    private bool canInteract = true; // Add this line

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        chorangSpriteRenderer = chorangTransform.GetComponent<SpriteRenderer>(); // Get Chorang's sprite renderer
    }

    void Update()
    {
        // Check if any horizontal input is detected
        float h = Input.GetAxisRaw("Horizontal");

        // If the player is grounded and no horizontal input is detected, freeze the velocity
        if (isGrounded && h == 0)
        {
            rigid.velocity = new Vector2(0f, rigid.velocity.y);
        }
        else
        {
            // Move horizontally
            rigid.velocity = new Vector2(h * maxSpeed, rigid.velocity.y);
        }

        // Flip sprite
        if (h != 0)
        {
            spriteRenderer.flipX = h < 0;
            // Also adjust Chorang's position based on the flip
            chorangTransform.localPosition = new Vector3(spriteRenderer.flipX ? -chorangOffsetX : chorangOffsetX,
                chorangTransform.localPosition.y, chorangTransform.localPosition.z);
            // Flip Chorang
            chorangSpriteRenderer.flipX = spriteRenderer.flipX;
        }

        anim.SetBool("isWalking", h != 0);

        if (isColliding && Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            StartCoroutine(InteractionCooldown(1.0f)); // Start the interaction cooldown immediately after the interaction key is pressed

            PuzzleController puzzleController = GetComponent<PuzzleController>();
            if (puzzleController != null)
            {
                StartCoroutine(puzzleController.HandlePuzzleInteraction(collidedObject.tag));
            }
        }
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

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.isTrigger && !hasLoggedOverlap) // Check if hasLoggedOverlap is false
        {
            isColliding = true;
            collidedObject = other.gameObject;
            hasLoggedOverlap = true; // Set hasLoggedOverlap to true after logging
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 충돌한 객체가 트리거 콜라이더인지 확인
        if (other.isTrigger)
        {
            isColliding = false;
            collidedObject = null;
            hasLoggedOverlap = false; // Reset hasLoggedOverlap to false when no longer overlapping
        }
    }
    
    IEnumerator InteractionCooldown(float duration)
    {
        canInteract = false;
        yield return new WaitForSeconds(duration);
        canInteract = true;
    }
}