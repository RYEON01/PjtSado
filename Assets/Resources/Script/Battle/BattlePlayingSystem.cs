using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BattlePlayingSystem : MonoBehaviour
{
    public static BattlePlayingSystem Instance { get; private set; }
    public BattleCharacter Player { get; set; }
    public BattleCharacter Enemy { get; set; }
    public BattleDialogue BattleDialogue { get; set; }
    public bool BufferFlag { get; set; }
    public bool ShielderFlag { get; set; }
    public Inventory Inventory { get; set; }
    public Button AttackButton;
    public Button BraceButton;
    public Button Brace_AssessButton;
    public Button Brace_ItemButton;
    public PentagonGraph PlayerPentagonGraph;
    public PentagonGraph EnemyPentagonGraph;
    public List<Button> ElementButtons;
    public List<Button> ElementDefButtons;
    public GameObject DialogueObj;
    public GameObject PentagonGraphUI;
    public GameObject TurnTutText;

    public TextMeshProUGUI PDamText;
    public TextMeshProUGUI EDefText;

    private bool isPlayerTurn;
    private bool previousTurn;
    public bool IsPlayerTurn
    {
        get { return isPlayerTurn; }
        set
        {
            isPlayerTurn = value;
            Debug.Log("IsPlayerTurn set to: " + value);
            if (isPlayerTurn)
            {
                HandlePlayerTurn();
            }
            else
            {
                HandleEnemyTurn();
            }
        }
    }

    public void IniSettings()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "Battle_C")
        {
            Destroy(this);
            return;
        }
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    
        AttackButton.onClick.AddListener(PlayerAttack);
        BraceButton.onClick.AddListener(PlayerBrace);
    }

    void OnDestroy()
    {
        if (AttackButton != null)
        {
            AttackButton.onClick.RemoveAllListeners();
        }

        if (BraceButton != null)
        {
            BraceButton.onClick.RemoveAllListeners();
        }
    }
    public void Initialize(BattleCharacter player, BattleCharacter enemy)
    {
        
        Player = player;
        Player.HP = 100;
        Enemy = enemy;
        Enemy.HP = 100;
        
        if (Enemy == null)
        {
            Debug.LogError("Enemy is null when trying to initialize BattlePlayingSystem");
            return;
        }
        
        if (Player == null)
        {
            Debug.LogError("Player is null when trying to initialize BattlePlayingSystem");
            return;
        }
        
        Debug.Log("BattlePlayingSystem initialized with enemy: " + Enemy.Name);
        
        IsPlayerTurn = true;
        BattleDialogue = ScriptableObject.CreateInstance<BattleDialogue>();
        BattleDialogue.Enemy = enemy;
        Inventory = ScriptableObject.CreateInstance<Inventory>();
        
        Debug.Log("BattlePlayingSystem initialized with enemy: " + Enemy.Name);
        Debug.Log("Enemy's Water stat: " + Enemy.WaterStat);
        Debug.Log("Enemy's Fire stat: " + Enemy.FireStat);
        Debug.Log("Enemy's Earth stat: " + Enemy.EarthStat);
        Debug.Log("Enemy's Wood stat: " + Enemy.WoodStat);
        Debug.Log("Enemy's Metal stat: " + Enemy.MetalStat);
    
        Inventory.AddItem(ScriptableObject.CreateInstance<Item_Healer>());
        Inventory.AddItem(ScriptableObject.CreateInstance<Item_Buffer>());
        Inventory.AddItem(ScriptableObject.CreateInstance<Item_Shielder>());
        
        BattleUIManager.Instance.PlayerPentagonGraph = PlayerPentagonGraph;
        BattleUIManager.Instance.EnemyPentagonGraph = EnemyPentagonGraph;
        BattleUIManager.Instance.PlayerHPSlider.maxValue = Player.HP;
        BattleUIManager.Instance.EnemyHPSlider.maxValue = Enemy.HP;
        BattleUIManager.Instance.PlayerHPSlider.value = Player.HP;
        BattleUIManager.Instance.EnemyHPSlider.value = Enemy.HP;
    }
    void Update()
    {
        if (TurnTutText.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
        {
            TurnTutText.SetActive(false);
            IsPlayerTurn = !IsPlayerTurn;
        }
    }

    public void HandlePlayerTurn()
    {
        Debug.Log("HandlePlayerTurn called");
        
        BattleUIManager.Instance.nameText.text = "TS0RVNG_II";
        BattleUIManager.Instance.dialogueText.text = "이매야, 이제 어쩌지?";
        
        DeactivateUI();
        
        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(AttackButton.gameObject, true, 1f));
        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(BraceButton.gameObject, true, 1f));
        AttackButton.enabled = true;
        BraceButton.enabled = true;
        AttackButton.onClick.AddListener(PlayerAttack);
        BraceButton.onClick.AddListener(PlayerBrace);
    }

    public void HandleEnemyTurn()
    {
        if (!ValidateEnemy())
        {
            Debug.LogError("Enemy is not valid");
            return;
        }
        
        Debug.Log("HandleEnemyTurn called");
    
        BattleUIManager.Instance.nameText.text = Enemy.Name;
        BattleUIManager.Instance.dialogueText.text = "호락호락하게 당할 것 같으냐!";
    
        DeactivateUI();
    
        // Define the elements array
        string[] elements = new string[] { "wood", "fire", "metal", "water", "earth" };

        Dictionary<Button, string> buttonToElementMap = new Dictionary<Button, string>();
        for (int i = 0; i < ElementDefButtons.Count; i++)
        {
            buttonToElementMap[ElementDefButtons[i]] = elements[i];
        }

        foreach (Button elementDefButton in ElementDefButtons)
        {
            StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(elementDefButton.gameObject, true, 1f));
            elementDefButton.onClick.RemoveAllListeners();
            string element = buttonToElementMap[elementDefButton];
            elementDefButton.onClick.AddListener(() => HandleElementDefense(element));
        }
    }
    
    public bool ValidateEnemy()
    {
        if (Enemy == null)
        {
            Debug.LogError("Enemy is null");
            return false;
        }

        if (Enemy.WaterStat == 0 || Enemy.FireStat == 0 || Enemy.EarthStat == 0 || Enemy.WoodStat == 0 || Enemy.MetalStat == 0)
        {
            Debug.LogError("Enemy stats are not initialized");
            return false;
        }
        Debug.Log("Enemy validated successfully");
        return true;
    }
    private void DeactivateUI()
    {
        Debug.Log("DeactivateUI called");
        
        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(EDefText.gameObject, false, 0f));
        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(PDamText.gameObject, false, 0f));
        
        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(BraceButton.gameObject, false, 0f));
        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(AttackButton.gameObject, false, 0f));
        
        PentagonGraphUI.gameObject.SetActive(false);
        
        foreach (Button elementButton in ElementButtons)
        {
            StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(elementButton.gameObject, false, 0f));
        }
    }

    public void PlayerAttack()
    {
        if (BraceButton == null)
        {
            Debug.LogError("BraceButton is null");
            return;
        }
        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(BraceButton.gameObject, false, 1f));

        List<string> elements = new List<string> { "wood", "fire", "metal", "water", "earth" };
        for (int i = 0; i < ElementButtons.Count; i++)
        {
            Button elementButton = ElementButtons[i];
            if (elementButton == null)
            {
                Debug.LogError("ElementButton at index " + i + " is null");
                continue;
            }
            string element = elements[i];

            StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(elementButton.gameObject, true, 1f));
            elementButton.onClick.RemoveAllListeners();
            elementButton.onClick.AddListener(() => HandleElementAttack(element));
        }
    }

    public void HandleElementAttack(string element)
    {
        if (Player == null)
        {
            Debug.LogError("Player is null in HandleElementAttack");
            return;
        }
            
        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(AttackButton.gameObject, false, 1f));
        foreach (Button elementButton in ElementButtons)
        {
            StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(elementButton.gameObject, false, 1f));
        }

        int PDam = Player.RollDice(element);
        Debug.Log("Player's " + element + " stat: " + GetPlayerStat(element));
        
        if (BufferFlag)
        {
            PDam += 15;
            BufferFlag = false;
        }

        int EDef = Enemy.RollDice(element);
        Debug.Log("Enemy's " + element + " stat: " + GetEnemyStat(element));

        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(PDamText.gameObject, true, 1f));
        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(EDefText.gameObject, true, 1f));
        PDamText.text = PDam.ToString();
        EDefText.text = EDef.ToString();
        PDamText.color = PDam == 1 ? Color.red : Color.white;
        EDefText.color = EDef == 1 ? Color.red : Color.white;

        if (PDam > EDef)
        {
            Enemy.HP -= (PDam - EDef);
        }
        else if (PDam == 1)
        {
            PDamText.color = Color.red;
        }
        else if (EDef == 1)
        {
            EDefText.color = Color.red;
            Enemy.HP -= (int)(1.2 * (PDam - EDef));
        }
        if (Player.HP <= 0)
        {
            GameOver();
        }
        if (Enemy.HP <= 0)
        {
            HandleBattleEnd();
        }

        BattleUIManager.Instance.UpdateUI();
        TurnTutText.SetActive(true);
    }

    public void HandleElementDefense(string element)
    {
        foreach (Button elementDefButton in ElementDefButtons)
        {
            StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(elementDefButton.gameObject, false, 1f));
        }

        string[] elements = new string[] { "wood", "fire", "metal", "water", "earth" };
        string enemyElement = elements[UnityEngine.Random.Range(0, elements.Length)];

        int EDam = Enemy.RollDice(enemyElement);
        Debug.Log("Enemy's " + element + " stat: " + GetEnemyStat(element));
            
        if (!Array.Exists(elements, el => el == element))
        {
            Debug.LogError("Invalid element: " + element);
            return;
        }
            
        int PDef = Player.RollDice(element);
        Debug.Log("Player's " + element + " stat: " + GetPlayerStat(element));

        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(PDamText.gameObject, true, 1f));
        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(EDefText.gameObject, true, 1f));
        EDefText.text = EDam.ToString();
        PDamText.text = PDef.ToString();
        EDefText.color = EDam == 1 ? Color.red : Color.white;
        PDamText.color = PDef == 1 ? Color.red : Color.white;
            
        if (ShielderFlag && PDef < EDam)
        {
            EDam -= 5;
            Enemy.HP -= 5;
            EDefText.text = EDam.ToString();
            ShielderFlag = false;
        }
            
        if (EDam > PDef)
        {
            Player.HP -= (EDam - PDef);
        }
        else if (EDam == 1)
        {
            EDefText.color = Color.red;
        }
        else if (PDef == 1)
        {
            PDamText.color = Color.red;
            Player.HP -= (int)(1.2 * (EDam - PDef));
        }

        if (Player.HP <= 0)
        {
            GameOver();
        }

        if (Enemy.HP <= 0)
        {
            HandleBattleEnd();
        }

        BattleUIManager.Instance.UpdateUI();
        TurnTutText.SetActive(true);
    }

    private int GetPlayerStat(string element)
    {
        element = element.ToLower();
        switch (element.ToLower())
        {
            case "water":
                return Player.WaterStat;
            case "fire":
                return Player.FireStat;
            case "earth":
                return Player.EarthStat;
            case "wood":
                return Player.WoodStat;
            case "metal":
                return Player.MetalStat;
            default:
                return 0;
        }
    }

    private int GetEnemyStat(string element)
    {
        element = element.ToLower();
        switch (element.ToLower())
        {
            case "water":
                return Enemy.WaterStat;
            case "fire":
                return Enemy.FireStat;
            case "earth":
                return Enemy.EarthStat;
            case "wood":
                return Enemy.WoodStat;
            case "metal":
                return Enemy.MetalStat;
            default:
                return 0;
        }
    }

    public void PlayerBrace()
    {
        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(AttackButton.gameObject, false, 1f));
        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(BraceButton.gameObject, false, 1f));
        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(Brace_AssessButton.gameObject, true, 1f));
        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(Brace_ItemButton.gameObject, true, 1f));
        Brace_AssessButton.onClick.AddListener(HandleBraceAssess);
        Brace_ItemButton.onClick.AddListener(HandleBraceItem);
    }
    
    public void HandleBraceAssess()
    {
        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(Brace_AssessButton.gameObject, false, 1f));
        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(Brace_ItemButton.gameObject, false, 1f));

        PentagonGraphUI.gameObject.SetActive(true);
        BattleUIManager.Instance.EnemyPentagonGraph.gameObject.SetActive(true);
        BattleUIManager.Instance.EnemyPentagonGraph.UpdateGraph(Enemy);

        BattleUIManager.Instance.nameText.text = "TS0RVNG_II";
        BattleUIManager.Instance.dialogueText.text = "이매야! 저 녀석의 운명을 분석해왔어. 어서 봐봐!";
        TurnTutText.SetActive(true);
    }

    public void HandleBraceItem()
    {
        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(Brace_AssessButton.gameObject, false, 1f));
        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(Brace_ItemButton.gameObject, false, 1f));

        if (Inventory == null || Inventory.Items.Count == 0)
        {
            Debug.Log("Inventory is null or empty");
        }
        else if (BattleUIManager.Instance != null)
        {
            BattleUIManager.Instance.PrintInventory(Inventory);
        }
    }

    public void UseItem(Item item)
    {
        if (item.Quantity <= 0)
        {
            Debug.Log("You don't have any more " + item.Name + " items left.");
            return;
        }
        
        Debug.Log("Using item: " + item.Name);
        
        item.Quantity--;
        item.Use();

        Button itemButton = null;
        if (item is Item_Healer)
        {
            itemButton = BattleUIManager.Instance.ItemHealerButton;
        }
        else if (item is Item_Buffer)
        {
            itemButton = BattleUIManager.Instance.ItemBufferButton;
        }
        else if (item is Item_Shielder)
        {
            itemButton = BattleUIManager.Instance.ItemShielderButton;
        }

        if (itemButton != null)
        {
            itemButton.interactable = item.Quantity > 0;
        }

        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(BattleUIManager.Instance.ItemHealerButton.gameObject, false, 1f));
        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(BattleUIManager.Instance.ItemBufferButton.gameObject, false, 1f));
        StartCoroutine(BattleUIManager.Instance.SetActiveWithFade(BattleUIManager.Instance.ItemShielderButton.gameObject, false, 1f));

        BattleUIManager.Instance.nameText.text = "TS0RVNG_II";
        BattleUIManager.Instance.dialogueText.text = "좋았어, 도움이 될 거야!";
        TurnTutText.SetActive(true);
    }


    public void HandleBattleEnd()
    {
        GameManager.Instance.HandleBattleEnd(Enemy);
    }
    
    public void GameOver()
    {
        Debug.LogWarning("GameOver.");
        SceneManager.LoadScene("GameOver");
    }
}