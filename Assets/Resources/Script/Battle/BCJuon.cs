using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCJuon : BattleCharacter
{
    public BCJuon()
    {
        Name = "Juon";
        Water = 15;
        Fire = 75;
        Earth = 25;
        Wood = 25;
        Metal = 30;
        Compassion = 0;
    }
    
    public override string GetDialogue()
    {
        return "BCJuon's dialogue";
    }
}
