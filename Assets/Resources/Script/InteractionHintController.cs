using UnityEngine;

public class InteractionHintController : MonoBehaviour
{
    public GameObject interactionHint; // Assign your InteractionHint sprite object in the inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactionHint.SetActive(true); // Enable the interaction hint
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactionHint.SetActive(false); // Disable the interaction hint
        }
    }
}