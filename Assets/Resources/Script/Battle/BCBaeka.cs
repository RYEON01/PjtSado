using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCBaeka : BattleCharacter
{
    public override void InitializeStats()
    {
        Name = "백아";
        WaterStat = 35;
        FireStat = 25;
        EarthStat = 25;
        WoodStat = 15;
        MetalStat = 90;
        Compassion = 0;
    }
    
    public override string GetDialogue()
    {
        return "BCBaeka's dialogue";
    }
}
