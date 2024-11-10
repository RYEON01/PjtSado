using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCBaeka : BattleCharacter
{
    public override void InitializeStats()
    {
        Name = "백아";
        WaterStat = 25;
        FireStat = 15;
        EarthStat = 15;
        WoodStat = 5;
        MetalStat = 60;
        Compassion = 0;
    }
    
    public override string GetDialogue()
    {
        return "BCBaeka's dialogue";
    }
}
