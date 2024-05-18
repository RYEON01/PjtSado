using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool isColliding;
    bool hasLoggedOverlap = false;
    public float maxSpeed;
    public float slopeForce;
    public float raycastLength;
    public Transform chorangTransform;
    public float chorangOffsetX;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    bool isGrounded;
    SpriteRenderer chorangSpriteRenderer;
    private List<GameObject> collidedObjects = new List<GameObject>();
    private bool canInteract = true;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        chorangSpriteRenderer = chorangTransform.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (isGrounded && h == 0)
        {
            rigid.velocity = new Vector2(0f, rigid.velocity.y);
        }
        else
        {
            rigid.velocity = new Vector2(h * maxSpeed, rigid.velocity.y);
        }

        if (h != 0)
        {
            spriteRenderer.flipX = h < 0;
            chorangTransform.localPosition = new Vector3(spriteRenderer.flipX ? -chorangOffsetX : chorangOffsetX,
                chorangTransform.localPosition.y, chorangTransform.localPosition.z);
            chorangSpriteRenderer.flipX = spriteRenderer.flipX;
        }

        anim.SetBool("isWalking", h != 0);

        if (collidedObjects.Count > 0 && Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            PuzzleController puzzleController = collidedObjects[collidedObjects.Count - 1].GetComponent<PuzzleController>();
            if (puzzleController != null && !puzzleController.IsInteracting() && !PuzzleManager.Instance.CheckSolution())
            {
                StartCoroutine(puzzleController.HandleInteraction());
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
        if (other.isTrigger && !collidedObjects.Contains(other.gameObject))
        {
            collidedObjects.Add(other.gameObject);
            hasLoggedOverlap = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            collidedObjects.Remove(other.gameObject);
            hasLoggedOverlap = false;
        }
    }
    
    IEnumerator InteractionCooldown(float duration)
    {
        canInteract = false;
        yield return new WaitForSeconds(duration);
        canInteract = true;
    }
}