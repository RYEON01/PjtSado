using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public BattlePlayingSystem BattleSystem { get; private set; }
    
    public BCPlayer Player;
    public BattleCharacter Enemy;
    public BattleCharacterType enemyCharacterType;
    
    private BattleUIManager UIManager;

    private bool OneShot = true;

    private void Awake()
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

        if (OneShot)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            OneShot = false;
        }
    }
    
    IEnumerator Start()
    {
        yield return null;

        if (Player != null)
        {
            LoadPlayerStats();
        }

        yield return null;
        if (SceneManager.GetActiveScene().name == "Battle_C")
        {
            BattleUIManager.Instance.UpdateUI();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene '{scene.name}' loaded.");
        
        BattleSetting();
        PlayerSetting();
        
        if (Player == null)
        {
            Debug.LogError("Player is null after loading scene " + scene.name);
            return;
        }
    
        if (!Player.StatsInitialized)
        {
            Player.LoadStats();
            Debug.Log("Player stats have been loaded successfully.");
        }

        if (Player.WaterStat == 0 && 
            Player.FireStat == 0 && 
            Player.EarthStat == 0 && 
            Player.WoodStat == 0 && 
            Player.MetalStat == 0)
        {
            Player.SetInitialStats();
            Debug.Log("Player stats have been initialized.");
        }

        if (scene.name == "Battle_C")
        {
            if (Enemy == null)
            {
                Enemy = CreateEnemyInstance(enemyCharacterType);
            }

            if (Enemy == null)
            {
                Debug.LogError("Enemy is null after loading scene " + scene.name);
                return;
            }

            Debug.Log("Initializing battle with Player: " + Player + " and Enemy: " + Enemy.Name);
            if (!Enemy.StatsInitialized)
            {
                Enemy.InitializeStats();
                Debug.Log("Enemy stats have been initialized.");
            }
            
            UIManager.InjectEnemy(Enemy);
            BattlePlayingSystem.Instance.Initialize(Player, Enemy);
            BattleUIManager.Instance.UpdateUI();
        }
    }
    
    private BattleCharacter CreateEnemyInstance(BattleCharacterType type)
    {
        GameObject enemyObject = new GameObject(type.ToString());
        BattleCharacter enemyInstance;
        
        switch (type)
        {
            case BattleCharacterType.BCCheongi:
                enemyInstance = enemyObject.AddComponent<BCCheongi>();
                break;
            case BattleCharacterType.BCJuon:
                enemyInstance = enemyObject.AddComponent<BCJuon>();
                break;
            case BattleCharacterType.BCBaeka:
                enemyInstance = enemyObject.AddComponent<BCBaeka>();
                break;
            case BattleCharacterType.BCMuksa:
                enemyInstance = enemyObject.AddComponent<BCMuksa>();
                break;
            default:
                Debug.LogError("Enemy type is unrecognized.");
                return null;
        }

        enemyInstance.InitializeStats();
        return enemyInstance;
    }
    
    void LoadPlayerStats()
    {
        if (Player == null)
        {
            Debug.LogError("Player is null in LoadPlayerStats");
            return;
        }

        if (!Player.StatsInitialized)
        {
            Player.LoadStats();
        }

        if (Player.WaterStat == 0 && Player.FireStat == 0 && Player.EarthStat == 0 && Player.WoodStat == 0 && Player.MetalStat == 0)
        {
            Player.SetInitialStats();
        }

        /*
        Debug.Log("Player stats after initial loading:");
        Debug.Log("WoodStat: " + Player.WoodStat);
        Debug.Log("FireStat: " + Player.FireStat);
        Debug.Log("MetalStat: " + Player.MetalStat);
        Debug.Log("WaterStat: " + Player.WaterStat);
        Debug.Log("EarthStat: " + Player.EarthStat);
        */
    }

    private void PlayerSetting()
    {
        Player = BCPlayer.PlayerInstance;
        if (Player != null)
        {
            Debug.LogWarning("Player instance: " + Player);
            DontDestroyOnLoad(Player);
            LoadPlayerStats();
        }
    }
    
    private void BattleSetting()
    {
        BattleSystem = FindObjectOfType<BattlePlayingSystem>();
        
        if (BattleSystem == null)
        {
            BattleSystem = gameObject.AddComponent<BattlePlayingSystem>();
        }
        
        BattleSystem.IniSettings();
        
        UIManager = FindObjectOfType<BattleUIManager>();
        if (UIManager != null)
        {
            UIManager.IniSettings();
        }
    }
    
    public void HandleBattleEnd(BattleCharacter enemy)
    {
        switch (enemy)
        {
            case BCCheongi:
                SceneManager.LoadScene("Cheongi_End");
                break;
            case BCJuon:
                SceneManager.LoadScene("Juon_End");
                break;
            case BCBaeka:
                SceneManager.LoadScene("Baeka_End");
                break;
            case BCMuksa:
                SceneManager.LoadScene("Muksa_End");
                break;
            default:
                Debug.LogError("Unknown enemy type. Can't transition to the end scene.");
                break;
        }
    }
}