using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    public BattleCharacter Player { get; set; }
    public BattleCharacter Enemy { get; set; }
    public PentagonGraph PentagonGraph { get; set; }

    public BattleUIManager(BattleCharacter player, BattleCharacter enemy)
    {
        Player = player;
        Enemy = enemy;
    }

    // Placeholder for the method that will update the UI
    public void UpdateUI()
    {
        PentagonGraph.UpdateGraph(Player);
    }
    
    public void ShowDialogue(string dialogue)
    {
        // TODO: Show the dialogue on the UI
    }

    public void ShowAnswerChoices(string[] choices)
    {
        // TODO: Show the answer choices on the UI
    }

    public IEnumerator SetActiveWithFade(GameObject obj, bool active, float fadeTime)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = obj.AddComponent<CanvasGroup>();
        }

        // Fade in before activating the object
        if (active)
        {
            obj.SetActive(true);
            for (float t = 0.02f; t < fadeTime; t += Time.deltaTime)
            {
                canvasGroup.alpha = Mathf.Lerp(0, 1, t / fadeTime);
                yield return null;
            }
        }
        // Fade out before deactivating the object
        else
        {
            for (float t = 0.02f; t < fadeTime; t += Time.deltaTime * 3)
            {
                canvasGroup.alpha = Mathf.Lerp(1, 0, t / fadeTime);
                yield return null;
            }
            obj.SetActive(false);
        }
    }
}