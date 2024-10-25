using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject dialogueUI;
    public Image chaImage;
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !hasTriggered)
        {
            if (DialogueManager.Instance != null && dialogue != null)
            {
                DialogueManager.Instance.PlayerInject(other.gameObject);
                DialogueManager.Instance.StartDialogue(dialogue, dialogueUI, chaImage);
                hasTriggered = true;
            }
            else
            {
                Debug.LogError("DialogueManager instance or dialogue is null");
            }
        }
    }
}