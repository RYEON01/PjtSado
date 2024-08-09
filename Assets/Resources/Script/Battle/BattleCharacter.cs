using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
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
    
    void Awake()
    {
        if (Name == "Imae") // Replace "Player" with the name of your player character
        {
            DontDestroyOnLoad(gameObject);
            SetInitialStats();
        }
    }

    public void SetInitialStats()
    {
        Water = 5 * Random.Range(1, 10) + 5;
        Fire = 5 * Random.Range(1, 10) + 5;
        Earth = 5 * Random.Range(1, 10) + 5;
        Wood = 5 * Random.Range(1, 10) + 5;
        Metal = 5 * Random.Range(1, 10) + 5;
        HP = 100;
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
}
