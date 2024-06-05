using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !hasTriggered) // Check if hasTriggered is false
        {
            if (DialogueManager.Instance != null && dialogue != null)
            {
                DialogueManager.Instance.StartDialogue(dialogue);
                hasTriggered = true; // Set hasTriggered to true after triggering the dialogue
            }
            else
            {
                Debug.LogError("DialogueManager instance or dialogue is null");
            }
        }
    }
}