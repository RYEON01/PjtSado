using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    public static BattleCharacter Instance { get; set; }
    public bool StatsInitialized { get; private set; }
    private static bool hasInitialized = false; // Add this line
    
    public string Name { get; set; }
    public int Water { get; set; }
    public int Fire { get; set; }
    public int Earth { get; set; }
    public int Wood { get; set; }
    public int Metal { get; set; }
    public int HP { get; set; }
    
    public int WoodElement { get; set; }
    public int FireElement { get; set; }
    public int EarthElement { get; set; }
    public int MetalElement { get; set; }
    public int WaterElement { get; set; }
    
    public int WaterStat { get; set; }
    public int FireStat { get; set; }
    public int EarthStat { get; set; }
    public int WoodStat { get; set; }
    public int MetalStat { get; set; }
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (!hasInitialized && WaterStat == 0 && FireStat == 0 && EarthStat == 0 && WoodStat == 0 && MetalStat == 0)
        {
            SetInitialStats();
            StatsInitialized = true;
            hasInitialized = true;
        }
    }

    public void SetInitialStats()
    {
        WaterStat = 10 * Random.Range(0, 4) + 10;
        FireStat = 10 * Random.Range(0, 4) + 10;
        EarthStat = 10 * Random.Range(0, 4) + 10;
        WoodStat = 10 * Random.Range(0, 4) + 10;
        MetalStat = 10 * Random.Range(0, 4) + 10;
        HP = 100;
        
        PentagonGraph pentagonGraph = FindObjectOfType<PentagonGraph>();
        if (pentagonGraph != null)
        {
            pentagonGraph.UpdateGraph(this);
        }
        StatsInitialized = true;
    }
    
    public int RollDice(string element)
    {
        int maxRoll = 0;
        switch (element.ToLower())
        {
            case "water":
                maxRoll = Water;
                break;
            case "fire":
                maxRoll = Fire;
                break;
            case "earth":
                maxRoll = Earth;
                break;
            case "wood":
                maxRoll = Wood;
                break;
            case "metal":
                maxRoll = Metal;
                break;
        }
        return Random.Range(1, maxRoll + 1);
    }
    
    public void SaveStats()
    {
        PlayerPrefs.SetInt("WaterStat", WaterStat);
        PlayerPrefs.SetInt("FireStat", FireStat);
        PlayerPrefs.SetInt("EarthStat", EarthStat);
        PlayerPrefs.SetInt("WoodStat", WoodStat);
        PlayerPrefs.SetInt("MetalStat", MetalStat);
        PlayerPrefs.Save();
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
        else if (!StatsInitialized)
        {
            SetInitialStats();
        }
    }
}
