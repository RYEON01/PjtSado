using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueTrigger_Battle : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject dialogueUI;
    public Image chaImage;

    public BattleCharacterType enemyCharacterType;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !hasTriggered)
        {
            if (DialogueManager.Instance != null && dialogue != null)
            {
                DialogueManager.Instance.PlayerInject(other.gameObject);
                DialogueManager.Instance.StartDialogue(dialogue, dialogueUI, chaImage);
                DialogueManager.Instance.DialogueEnded += OnDialogueEnded; // Subscribe to the event
                hasTriggered = true;
            }
            else
            {
                Debug.LogError("DialogueManager instance or dialogue is null");
            }
        }
    }

    private void OnDialogueEnded()
    {
        DialogueManager.Instance.DialogueEnded -= OnDialogueEnded;
        GameManager.Instance.enemyCharacterType = enemyCharacterType;

        StartCoroutine(StartBattleCoroutine());
    }
    
    public void Disable()
    {
        hasTriggered = true;
    }

    private IEnumerator StartBattleCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        //Debug.Log("Starting battle sequence...");

        if (GameManager.Instance == null)
        {
            Debug.Log("Creating a new GameManager instance.");
            GameObject gameManagerObject = new GameObject("GameManager");
            gameManagerObject.AddComponent<GameManager>();
            DontDestroyOnLoad(gameManagerObject);
        }

        Type type = Type.GetType(enemyCharacterType.ToString());

        if (type != null)
        {
            GameObject enemyObject = new GameObject(enemyCharacterType.ToString());
            BattleCharacter enemyInstance = (BattleCharacter)enemyObject.AddComponent(type);
            
            if (enemyInstance != null)
            {
                enemyInstance.InitializeStats();
                GameManager.Instance.Enemy = enemyInstance; // Assign to GameManager
                Debug.Log("Enemy successfully created and stored in GameManager.");

                // Now switch to the battle scene
                SceneManager.LoadScene("Battle_C", LoadSceneMode.Single);
            }
            else
            {
                Debug.LogError("Failed to instantiate BattleCharacter for: " + enemyCharacterType.ToString());
            }
        }
        else
        {
            Debug.LogError($"Could not find type for {enemyCharacterType}. Ensure the name is correct.");
        }
    }
}
