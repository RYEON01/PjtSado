using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCCheongi : BattleCharacter
{
    public override void InitializeStats()
    {
        Name = "청이";
        WaterStat = 65;
        FireStat = 30;
        EarthStat = 20;
        WoodStat = 25;
        MetalStat = 10;
        Compassion = 0;
        
        /*
        Debug.Log("BCCheongi stats initialized");
        Debug.Log("Water: " + WaterStat);
        Debug.Log("Fire: " + FireStat);
        Debug.Log("Earth: " + EarthStat);
        Debug.Log("Wood: " + WoodStat);
        Debug.Log("Metal: " + MetalStat);
        Debug.Log("Compassion: " + Compassion);
        */
    }
    
    public override string GetDialogue()
    {
        return "BCCheongi's dialogue";
    }
}