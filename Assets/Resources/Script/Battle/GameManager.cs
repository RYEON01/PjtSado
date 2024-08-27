using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public BattlePlayingSystem BattleSystem { get; private set; }
    public BattleCharacter Player;

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
            return;
        }

        BattleSystem = GetComponent<BattlePlayingSystem>();
        if (BattleSystem == null)
        {
            BattleSystem = gameObject.AddComponent<BattlePlayingSystem>();
        }
        else
        {
            BattleSystem = GetComponent<BattlePlayingSystem>();
        }

        BattleSystem.Initialize(BattleCharacter.Instance, BattleCharacter.Instance);
        Player = BattleCharacter.Instance;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!BattleCharacter.Instance.StatsInitialized)
        {
            BattleCharacter.Instance.LoadStats();
        }
        Player.LoadStats();

        if (Player.WaterStat == 0 && Player.FireStat == 0 && Player.EarthStat == 0 && Player.WoodStat == 0 && Player.MetalStat == 0)
        {
            Player.SetInitialStats();
        }
    }

    public void ConfirmStats()
    {
        Player.SaveStats();
        Player.LoadStats();
        BattleSystem.Player = Player;
        BattleSystem.Enemy = BattleCharacter.Instance;
    }
}