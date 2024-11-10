using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCJuon : BattleCharacter
{
    public override void InitializeStats()
    {
        Name = "주온";
        WaterStat = 5;
        FireStat = 55;
        EarthStat = 15;
        WoodStat = 15;
        MetalStat = 20;
        Compassion = 0;
    }
    
    public override string GetDialogue()
    {
        return "BCJuon's dialogue";
    }
}
