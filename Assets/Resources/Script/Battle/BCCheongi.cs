using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCCheongi : BattleCharacter
{
    public BCCheongi()
    {
        Name = "Juon";
        Water = 65;
        Fire = 30;
        Earth = 20;
        Wood = 25;
        Metal = 10;
        Compassion = 0;
    }
    
    public override string GetDialogue()
    {
        return "BCCheongi's dialogue";
    }
}
