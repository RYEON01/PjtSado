using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCPlayer : BattleCharacter
{
    private static BCPlayer playerInstance;
    private static bool hasInitialized = false;

    public static BCPlayer PlayerInstance
    {
        get
        {
            if (playerInstance == null)
            {
                playerInstance = FindObjectOfType<BCPlayer>();
                if (playerInstance == null)
                {
                    GameObject obj = new GameObject("BCPlayer");
                    playerInstance = obj.AddComponent<BCPlayer>();
                    DontDestroyOnLoad(obj);
                }
            }
            return playerInstance;
        }
    }

    void Awake()
    {
        Debug.Log("BCPlayer Awake method called.");
        if (playerInstance != null && playerInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        if (playerInstance == null)
        {
            playerInstance = this;
        }

        Debug.Log("playerInstance object: " + playerInstance);
        DontDestroyOnLoad(this.gameObject);
        
        if (!hasInitialized && WaterStat == 0 && FireStat == 0 && EarthStat == 0 && WoodStat == 0 && MetalStat == 0)
        {
            if (!StatsInitialized)
            {
                LoadStats();
            }
            hasInitialized = true;
        }
    }

    public void SetInitialStats()
    {
        WaterStat = 20;
        FireStat = 30;
        EarthStat = 40;
        WoodStat = 20;
        MetalStat = 20;
        HP = 100;
        
        PentagonGraph pentagonGraph = FindObjectOfType<PentagonGraph>();
        if (pentagonGraph != null)
        {
            pentagonGraph.UpdateGraph(this);
        }
        
        StatsInitialized = true;
        SaveStats();
    }

    public void SaveStats()
    {
        PlayerPrefs.SetInt("WaterStat", WaterStat);
        PlayerPrefs.SetInt("FireStat", FireStat);
        PlayerPrefs.SetInt("EarthStat", EarthStat);
        PlayerPrefs.SetInt("WoodStat", WoodStat);
        PlayerPrefs.SetInt("MetalStat", MetalStat);
        PlayerPrefs.Save();
        
        PentagonGraph?.UpdateGraph(this);
    }

    public void LoadStats()
    {
        if (PlayerPrefs.HasKey("WaterStat"))
        {
            WaterStat = PlayerPrefs.GetInt("WaterStat");
            FireStat = PlayerPrefs.GetInt("FireStat");
            EarthStat = PlayerPrefs.GetInt("EarthStat");
            WoodStat = PlayerPrefs.GetInt("WoodStat");
            MetalStat = PlayerPrefs.GetInt("MetalStat");
        
            PentagonGraph pentagonGraph = FindObjectOfType<PentagonGraph>();
            if (pentagonGraph != null)
            {
                pentagonGraph.UpdateGraph(this);
            }
        }
        else
        {
            Debug.Log("PlayerPrefs keys not found, setting initial stats");
            SetInitialStats();
        
            PentagonGraph pentagonGraph = FindObjectOfType<PentagonGraph>();
            if (pentagonGraph != null)
            {
                pentagonGraph.UpdateGraph(this);
            }
        }
    }
}
