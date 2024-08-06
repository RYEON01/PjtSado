using UnityEngine;
using System.Collections.Generic;

public class CutsceneTrigger : MonoBehaviour
{
    public List<string> dialogueLines = new List<string> { "안녕하세요", "Line 2", "Line 3" }; // Write your dialogue lines here
    public string nextSceneName;

    private void Start()
    {
        CutsceneManager.Instance.nextSceneName = nextSceneName;
        CutsceneManager.Instance.StartCutscene(dialogueLines);
    }
}