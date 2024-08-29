using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIManager : MonoBehaviour
{
    public BattleCharacter Player { get; set; }
    public BattleCharacter Enemy { get; set; }
    public PentagonGraph PentagonGraph { get; set; }

    public TMP_Text dialogueText;
    public TMP_Text nameText;
    public TMP_Text tutText;

    private GameObject dialogueUI;
    
    public Button ItemHealerButton;
    public Button ItemBufferButton;
    public Button ItemShielderButton;

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

    public void PrintInventory(Inventory inventory)
    {
        if (inventory == null)
        {
            Debug.Log("Inventory is null");
            return;
        }

        if (inventory.Items == null)
        {
            Debug.Log("Inventory.Items is null");
            return;
        }

        if (dialogueText == null)
        {
            Debug.Log("dialogueText is null");
            return;
        }

        nameText.text = "TS0RVNG";

        string message = "이매야! 지금 ";

        foreach (Item item in inventory.Items)
        {
            message += item.Name + " " + item.Quantity + "개, ";
        }
        message = message.TrimEnd(',', ' ') + "를 쓸 수 있어. 필요한 걸 말해줘!";

        dialogueText.text = message;
        
        foreach (Item item in inventory.Items)
        {
            Button itemButton = null;
            if (item is Item_Healer)
            {
                itemButton = ItemHealerButton;
            }
            else if (item is Item_Buffer)
            {
                itemButton = ItemBufferButton;
            }
            else if (item is Item_Shielder)
            {
                itemButton = ItemShielderButton;
            }

            if (itemButton != null)
            {
                itemButton.gameObject.SetActive(true);
                itemButton.interactable = item.Quantity > 0;
                itemButton.onClick.RemoveAllListeners();
                itemButton.onClick.AddListener(() => BattlePlayingSystem.Instance.UseItem(item));
            }
        }
    }
}