using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BattlePlayingSystem : MonoBehaviour
{
    public static BattlePlayingSystem Instance { get; private set; }
    public BattleCharacter Player { get; set; }
    public BattleCharacter Enemy { get; set; }
    public bool IsPlayerTurn { get; set; }
    public BattleDialogue BattleDialogue { get; set; }
    public Inventory Inventory { get; set; }
    public Button AttackButton;
    public Button BraceButton;
    public List<Button> ElementButtons;

    public TextMeshProUGUI PDamText;
    public TextMeshProUGUI EDefText;

    public BattleUIManager UIManager; // Add this line

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UIManager = GameObject.FindObjectOfType<BattleUIManager>();
        if (UIManager == null)
        {
            Debug.LogError("BattleUIManager instance not found");
            return;
        }
        AttackButton.onClick.AddListener(PlayerAttack);
        BraceButton.onClick.AddListener(PlayerAttack);
    }

    void OnDestroy()
    {
        AttackButton.onClick.RemoveAllListeners();
        BraceButton.onClick.RemoveAllListeners();
    }
    public void Initialize(BattleCharacter player, BattleCharacter enemy)
    {
        Player = player;
        Player.HP = 100;
        Enemy = enemy;
        Enemy.HP = 100;
        IsPlayerTurn = true;
        BattleDialogue = new BattleDialogue(enemy);
        Inventory = new Inventory();
    }
    public void HandleTurn(string playerElement, string enemyElement)
    {
        BattleCharacter attacker = IsPlayerTurn ? Player : Enemy;
        BattleCharacter defender = IsPlayerTurn ? Enemy : Player;

        int attackRoll = attacker.RollDice(playerElement);
        int defendRoll = defender.RollDice(enemyElement);

        if (attackRoll > defendRoll)
        {
            defender.HP -= (attackRoll - defendRoll);
        }

        IsPlayerTurn = !IsPlayerTurn;
    }

    public void HandlePlayerTurn()
    {
        BattleDialogue.StartDialogue();
        StartCoroutine(UIManager.SetActiveWithFade(AttackButton.gameObject, true, 1f));
        StartCoroutine(UIManager.SetActiveWithFade(BraceButton.gameObject, true, 1f));
        AttackButton.enabled = true;
        BraceButton.enabled = true;
        AttackButton.onClick.AddListener(PlayerAttack);
        BraceButton.onClick.AddListener(PlayerAttack);
    }

    public void HandleEnemyTurn()
    {
        BattleDialogue.StartDialogue();
        EnemyAttack();
    }

    public void PlayerAttack()
    {
        StartCoroutine(UIManager.SetActiveWithFade(BraceButton.gameObject, false, 1f));

        List<string> elements = new List<string> { "wood", "fire", "metal", "water", "earth" };
        for (int i = 0; i < ElementButtons.Count; i++)
        {
            Button elementButton = ElementButtons[i];
            string element = elements[i];

            StartCoroutine(UIManager.SetActiveWithFade(elementButton.gameObject, true, 1f)); // Show the element button
            elementButton.onClick.RemoveAllListeners();
            elementButton.onClick.AddListener(() => HandleElementAttack(element));
        }
    }

    public void HandleElementAttack(string element)
    {
        StartCoroutine(UIManager.SetActiveWithFade(AttackButton.gameObject, false, 1f));
        foreach (Button elementButton in ElementButtons)
        {
            StartCoroutine(UIManager.SetActiveWithFade(elementButton.gameObject, false, 1f));
        }

        int playerStat = GetPlayerStat(element);
        Debug.Log("Player stat for " + element + ": " + playerStat);
        int PDam = Player.RollDice(element);
        Debug.Log("PDam: " + PDam);
        StartCoroutine(UIManager.SetActiveWithFade(PDamText.gameObject, true, 1f));
        PDamText.text = PDam.ToString();

        int enemyStat = GetEnemyStat(element);
        Debug.Log("Enemy stat for " + element + ": " + enemyStat);
        int EDef = Enemy.RollDice(element);
        Debug.Log("EDef: " + EDef);
        StartCoroutine(UIManager.SetActiveWithFade(EDefText.gameObject, true, 1f));
        EDefText.text = EDef.ToString();

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

        if (Enemy.HP <= 0)
        {
            HandleBattleEnd();
        }

        IsPlayerTurn = !IsPlayerTurn;
    }

    private int GetPlayerStat(string element)
    {
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
        switch (element.ToLower())
        {
            case "water":
                return Enemy.FireStat;
            case "fire":
                return Enemy.MetalStat;
            case "earth":
                return Enemy.WaterStat;
            case "wood":
                return Enemy.EarthStat;
            case "metal":
                return Enemy.WoodStat;
            default:
                return 0;
        }
    }

    public void PlayerBrace()
    {
        // TODO: Implement the player's brace
    }

    public void EnemyAttack()
    {
        // TODO: Implement the enemy's attack
    }

    public void HandleBattleEnd()
    {
        // TODO: Implement the ending phase of the battle
    }
}