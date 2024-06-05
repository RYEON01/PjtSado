using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public GameObject dialogueUI;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    private Queue<string> sentences;
    private bool isDialogueActive;
    
    private PlayerMove playerMove;

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
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogueUI != null && nameText != null && dialogueText != null && dialogue != null)
        {
            dialogueUI.SetActive(true);
            isDialogueActive = true;

            nameText.text = dialogue.name;

            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

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
    }

    private void EndDialogue()
    {
        dialogueUI.SetActive(false);
        isDialogueActive = false;
        
        playerMove.SetPlayerMovement(true);
    }

    private void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.F))
        {
            DisplayNextSentence();
        }
    }
}