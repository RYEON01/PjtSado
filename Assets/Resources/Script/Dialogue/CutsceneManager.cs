using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance { get; private set; }

    public TMP_Text dialogueText;
    public Image fadeOverlay;
    public string nextSceneName;

    private Queue<string> sentences;

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

    public void StartCutscene(List<string> dialogueLines)
    {
        sentences.Clear();

        foreach (string sentence in dialogueLines)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            StartCoroutine(TransitionToNextScene());
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    private IEnumerator TransitionToNextScene()
    {
        float transitionTime = 1f;
        float t = 0;

        // Fade out
        while (t < transitionTime)
        {
            t += Time.deltaTime;
            float alpha = t / transitionTime;
            fadeOverlay.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);

        // Reset t
        t = 0;

        // Fade in
        while (t < transitionTime)
        {
            t += Time.deltaTime;
            float alpha = 1 - (t / transitionTime);
            fadeOverlay.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}