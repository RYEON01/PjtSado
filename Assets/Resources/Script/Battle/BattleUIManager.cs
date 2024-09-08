using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIManager : MonoBehaviour
{
    public static BattleUIManager Instance { get; private set; }
    public BattleCharacter Player { get; set; }
    public BattleCharacter Enemy { get; set; }
    
    public PentagonGraph PlayerPentagonGraph { get; set; }
    public PentagonGraph EnemyPentagonGraph { get; set; }

    public TMP_Text dialogueText;
    public TMP_Text nameText;
    public TMP_Text tutText;

    private GameObject dialogueUI;
    
    public UnityEngine.UI.Slider PlayerHPSlider;
    public UnityEngine.UI.Slider EnemyHPSlider;
    public TMP_Text PlayerHPText;
    public TMP_Text EnemyHPText;
    
    public Button ItemHealerButton;
    public Button ItemBufferButton;
    public Button ItemShielderButton;

    public void IniSettings()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance is null");
        }
        else
        {
            Player = GameManager.Instance.Player;
            Enemy = GameManager.Instance.Enemy;
        }
        if (Player == null)
        {
            Debug.LogError("Player is null");
        }

        if (Enemy == null)
        {
            Debug.LogError("Enemy is null");
        }
    }
    public BattleUIManager(BattleCharacter player, BattleCharacter enemy)
    {
        Player = player;
        Enemy = enemy;
    }

    public void UpdateUI()
    {
        Debug.Log("Player object before UpdateUI: " + Player);
        Debug.Log("Enemy object before UpdateUI: " + Enemy);
        
        if (Player == null)
        {
            Debug.LogError("Player is null at the beginning of UpdateUI");
        }
        else
        {
            Debug.Log("Player is not null at the beginning of UpdateUI");
        }

        if (Enemy == null)
        {
            Debug.LogError("Enemy is null");
            return;
        }
        
        if (PlayerHPSlider == null)
        {
            Debug.LogError("PlayerHPSlider is null");
            return;
        }
        PlayerHPSlider.value = Player.HP;

        if (EnemyHPSlider == null)
        {
            Debug.LogError("EnemyHPSlider is null");
            return;
        }
        EnemyHPSlider.value = Enemy.HP;

        if (PlayerHPText == null)
        {
            Debug.LogError("PlayerHPText is null");
            return;
        }
        PlayerHPText.text = $"{Player.HP}/100";

        if (EnemyHPText == null)
        {
            Debug.LogError("EnemyHPText is null");
            return;
        }
        EnemyHPText.text = $"{Enemy.HP}/100";
        
        Debug.Log("Player object after UpdateUI: " + Player);
        Debug.Log("Enemy object after UpdateUI: " + Enemy);
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