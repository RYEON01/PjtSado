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
    
    public Image BCCheongiSprite;
    public Image BCJuonSprite;
    public Image BCBaekaSprite;
    public Image BCMuksaSprite;
    
    public Button startTutorialButton;
    public GameObject[] tutorialPages;
    public GameObject gradationBlack;
    public Button nextButton;
    public Button previousButton;
    public Button exitButton;
    private int currentPageIndex = 0;
    private bool tutFlag = true;


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
        }
    }
    
    public void InjectEnemy(BattleCharacter enemy)
    {
        Enemy = enemy;
    }

    public void UpdateUI()
    {
        if (PlayerHPSlider == null || EnemyHPSlider == null || PlayerHPText == null || EnemyHPText == null)
        {
            Debug.LogError("UI components are not assigned in BattleUIManager.");
            return;
        }

        if (Player == null)
        {
            Debug.LogError("Player reference is null in BattleUIManager.");
            return;
        }
        
        if (Enemy == null)
        {
            Debug.LogError("Enemy reference is null in BattleUIManager.");
            return;
        }
        if (startTutorialButton != null)
        {
            if (tutFlag)
            {
                startTutorialButton.onClick.AddListener(InitializeTutorial);
                tutFlag = false;
            }
        }

        PlayerHPSlider.value = Player.HP;
        EnemyHPSlider.value = Enemy.HP;
        PlayerHPText.text = $"{Player.HP}/100";
        EnemyHPText.text = $"{Enemy.HP}/100";
       
        switch (Enemy.GetType().Name)
        {
            case "BCCheongi":
                BCCheongiSprite.gameObject.SetActive(true);
                break;
            case "BCJuon":
                BCJuonSprite.gameObject.SetActive(true);
                break;
            case "BCBaeka":
                BCBaekaSprite.gameObject.SetActive(true);
                break;
            case "BCMuksa":
                BCMuksaSprite.gameObject.SetActive(true);
                break;
        }
        
        Debug.Log("Player object after UpdateUI: " + Player);
        Debug.Log("Enemy object after UpdateUI: " + Enemy);
    }

    public IEnumerator SetActiveWithFade(GameObject obj, bool active, float fadeTime)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = obj.AddComponent<CanvasGroup>();
        }
        
        if (active)
        {
            obj.SetActive(true);
            for (float t = 0.02f; t < fadeTime; t += Time.deltaTime)
            {
                canvasGroup.alpha = Mathf.Lerp(0, 1, t / fadeTime);
                yield return null;
            }
            canvasGroup.alpha = 1;
        }
        
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

        nameText.text = "TS0RVNG_II";

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
                
                CanvasGroup canvasGroup = itemButton.GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = itemButton.gameObject.AddComponent<CanvasGroup>();
                }
                canvasGroup.alpha = item.Quantity > 0 ? 1f : 0.2f;
                canvasGroup.interactable = item.Quantity > 0;
            }
        }
    }
    
    public void InitializeTutorial()
    {
        gradationBlack.SetActive(true);
        foreach (var page in tutorialPages)
        {
            page.SetActive(false);
        }
        if (tutorialPages.Length > 0)
        {
            tutorialPages[0].SetActive(true);
        }
        exitButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
        previousButton.gameObject.SetActive(true);
        
        nextButton.onClick.RemoveAllListeners();
        previousButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
        
        nextButton.onClick.AddListener(OnNextPage);
        previousButton.onClick.AddListener(OnPreviousPage);
        exitButton.onClick.AddListener(OnExitTutorial);

        UpdateTutorialButtonVisibility();
        
        Debug.Log($"InitializeTutorial Loaded.");
    }

    public void OnNextPage()
    {
        Debug.Log("OnNext Loaded.");
        tutorialPages[currentPageIndex].SetActive(false);
        currentPageIndex = Mathf.Min(currentPageIndex + 1, tutorialPages.Length - 1);
        tutorialPages[currentPageIndex].SetActive(true);
        UpdateTutorialButtonVisibility();
    }

    public void OnPreviousPage()
    {
        Debug.Log("OnPrevious Loaded.");
        tutorialPages[currentPageIndex].SetActive(false);
        currentPageIndex = Mathf.Max(currentPageIndex - 1, 0);
        tutorialPages[currentPageIndex].SetActive(true);
        UpdateTutorialButtonVisibility();
    }

    public void OnExitTutorial()
    {
        Debug.Log("OnExit Loaded.");
        foreach (var page in tutorialPages)
        {
            page.SetActive(false);
        }
        currentPageIndex = 0;
        gradationBlack.SetActive(false);
        exitButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        previousButton.gameObject.SetActive(false);
    }

    public void UpdateTutorialButtonVisibility()
    {
        exitButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(currentPageIndex < tutorialPages.Length - 1);
        previousButton.gameObject.SetActive(currentPageIndex > 0);
        Debug.Log($"Previous Button Active: {previousButton.gameObject.activeSelf}, Next Button Active: {nextButton.gameObject.activeSelf}, Exit Button Active: {exitButton.gameObject.activeSelf}");
    }
}