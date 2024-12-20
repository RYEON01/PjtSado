using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    private GameObject dialogueUI;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public TMP_Text tutorialText;
    private Dialogue currentDialogue;

    private Queue<string> sentences;
    private bool isDialogueActive;
    private PlayerMove playerMove;

    // Declare the DialogueEnded event
    public event Action DialogueEnded;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        sentences = new Queue<string>();
    }

    public void PlayerInject(GameObject player)
    {
        playerMove = player.GetComponent<PlayerMove>();
    }

    public void StartDialogue(Dialogue dialogue, GameObject dialogueUI, Image chaImage)
    {
        currentDialogue = dialogue;
        this.dialogueUI = dialogueUI;
        
        if (dialogueUI != null && nameText != null && dialogueText != null && dialogue != null)
        {
            dialogueUI.SetActive(true);
            isDialogueActive = true;
            
            nameText.text = dialogue.chaName;
            chaImage.gameObject.SetActive(true);
            
            DialogueTrigger dialogueTrigger = dialogueUI.GetComponent<DialogueTrigger>();
            if (dialogueTrigger != null && dialogueTrigger.chaImage != null)
            {
                dialogueTrigger.chaImage.gameObject.SetActive(true);
            }

            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
        
            tutorialText.gameObject.SetActive(true);

            DisplayNextSentence();
            playerMove.SetPlayerMovement(false);
        }
        else
        {
            Debug.LogError("Dialogue UI, name text, dialogue text, or dialogue is null");
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

        if (sentences.Count == currentDialogue.sentences.Count - 2)
        {
            tutorialText.gameObject.SetActive(false);
        }
    }

    private void EndDialogue()
    {
        dialogueUI.SetActive(false);
        isDialogueActive = false;
        
        DialogueTrigger dialogueTrigger = dialogueUI.GetComponent<DialogueTrigger>();
        if (dialogueTrigger != null && dialogueTrigger.chaImage != null)
        {
            dialogueTrigger.chaImage.gameObject.SetActive(false);
        }
        
        playerMove.SetPlayerMovement(true);

        // Invoke the DialogueEnded event
        DialogueEnded?.Invoke();
    }

    private void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }
}
