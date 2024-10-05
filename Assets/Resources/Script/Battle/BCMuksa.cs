using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCMuksa : BattleCharacter
{
    public override void InitializeStats()
    {
        Name = "묵사";
        WaterStat = 110;
        FireStat = 25;
        EarthStat = 20;
        WoodStat = 25;
        MetalStat = 30;
        Compassion = 0;
    }
    
    public override string GetDialogue()
    {
        return "BCMuksa's dialogue";
    }
}
