using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCJuon : BattleCharacter
{
    public override void InitializeStats()
    {
        Name = "주온";
        WaterStat = 15;
        FireStat = 75;
        EarthStat = 25;
        WoodStat = 25;
        MetalStat = 30;
        Compassion = 0;
    }
    
    public override string GetDialogue()
    {
        return "BCJuon's dialogue";
    }
}
