using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCMuksa : BattleCharacter
{
    public override void InitializeStats()
    {
        Name = "묵사";
        WaterStat = 65;
        FireStat = 15;
        EarthStat = 10;
        WoodStat = 15;
        MetalStat = 20;
        Compassion = 0;
    }
    
    public override string GetDialogue()
    {
        return "BCMuksa's dialogue";
    }
}
