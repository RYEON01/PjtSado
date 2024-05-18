using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public bool isColliding;
    public Transform chorangTransform;
    public float chorangOffsetX;
    SpriteRenderer spriteRenderer;
    Animator anim;
    SpriteRenderer chorangSpriteRenderer;
    private List<GameObject> collidedObjects = new List<GameObject>();
    private bool canInteract = true;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        chorangSpriteRenderer = chorangTransform.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

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

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.isTrigger && !collidedObjects.Contains(other.gameObject))
        {
            collidedObjects.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            collidedObjects.Remove(other.gameObject);
        }
    }
    
    IEnumerator InteractionCooldown(float duration)
    {
        canInteract = false;
        yield return new WaitForSeconds(duration);
        canInteract = true;
    }
}