using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCBaeka : BattleCharacter
{
    public BCBaeka()
    {
        Name = "Cheongi";
        Water = 35;
        Fire = 25;
        Earth = 25;
        Wood = 15;
        Metal = 90;
        Compassion = 0;
    }
    
    public override string GetDialogue()
    {
        return "BCBaeka's dialogue";
    }
}
