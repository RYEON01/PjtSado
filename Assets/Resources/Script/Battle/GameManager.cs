using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public BattlePlayingSystem BattleSystem { get; private set; }
    
    public BCPlayer Player;
    public BattleCharacter Enemy;
    
    private BattleUIManager UIManager;
    
    private static string lastFinishedDialogue = "default";
    private string GetLastFinishedDialogue()
    {
        return lastFinishedDialogue;
    }

    void Awake()
    {
        Debug.Log("GameManager Awake method called");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        
        Player = BCPlayer.PlayerInstance;
        if (Player != null)
        {
            DontDestroyOnLoad(Player);
            LoadPlayerStats();
        }

        BattleSystem = GetComponent<BattlePlayingSystem>();
        UIManager = GetComponent<BattleUIManager>();
        UIManager.IniSettings();
        
        if (BattleSystem == null)
        {
            BattleSystem = gameObject.AddComponent<BattlePlayingSystem>();
        }
        
        string finishedDialogue = GetLastFinishedDialogue();
        switch (finishedDialogue)
        {
            case "DCheongi":
                StartBattle<BCCheongi>();
                break;
            case "DJuon":
                StartBattle<BCJuon>();
                break;
            case "DBaeka":
                StartBattle<BCBaeka>();
                break;
            case "DMuksa":
                StartBattle<BCMuksa>();
                break;
            default:
                StartBattle<BCCheongi>();
                break;
        }
        
        if (Player != null && Enemy != null)
        {
            BattleSystem.Initialize(Player, Enemy);
        }
        Debug.Log("Enemy object: " + Enemy);
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        Debug.Log("Player object after Awake: " + Player);
        Debug.Log("Enemy object after Awake: " + Enemy);
    }
    
    IEnumerator Start()
    {
        // Wait for the next frame to make sure all Awake methods are called
        yield return null;

        if (Player != null)
        {
            LoadPlayerStats();
        }

        // Wait for the next frame to make sure Player is initialized
        yield return null;

        BattleUIManager.Instance.UpdateUI();
        Debug.Log("Player object after Start: " + Player);
        Debug.Log("Enemy object after Start: " + Enemy);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Player == null)
        {
            Debug.LogError("Player is null after loading scene " + scene.name);
        }
        else
        {
            if (!Player.StatsInitialized)
            {
                Player.LoadStats();
            }

            if (Player.WaterStat == 0 && Player.FireStat == 0 && Player.EarthStat == 0 && Player.WoodStat == 0 && Player.MetalStat == 0)
            {
                Player.SetInitialStats();
            }
            /*
            Debug.Log("Player stats after loading scene " + scene.name + ":");
            Debug.Log("WoodStat: " + Player.WoodStat);
            Debug.Log("FireStat: " + Player.FireStat);
            Debug.Log("MetalStat: " + Player.MetalStat);
            Debug.Log("WaterStat: " + Player.WaterStat);
            Debug.Log("EarthStat: " + Player.EarthStat);
            */
        }
        BattleUIManager.Instance.UpdateUI();
        Debug.Log("Player object after OnSceneLoaded: " + Player);
        Debug.Log("Enemy object after OnSceneLoaded: " + Enemy);
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

    public void ConfirmStats()
    {
        Player.SaveStats();
        Player.LoadStats();
        BattleSystem.Player = Player;
        BattleSystem.Enemy = BattleCharacter.Instance;
    }
    private void StartBattle<T>() where T : BattleCharacter
    {
        GameObject enemyObject = new GameObject("Enemy");
        T enemyInstance = enemyObject.AddComponent<T>();
        GameManager.Instance.BattleSystem.Enemy = enemyInstance;
        BattleCharacter.EnemyInstance = enemyInstance;
        
        Debug.Log("StartBattle method called. Enemy object: " + Enemy);
    }
}