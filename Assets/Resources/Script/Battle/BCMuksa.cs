using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCMuksa : BattleCharacter
{
    public BCMuksa()
    {
        Name = "묵사";
        Water = 110;
        Fire = 25;
        Earth = 20;
        Wood = 25;
        Metal = 30;
        Compassion = 0;
    }
    
    public override string GetDialogue()
    {
        return "BCMuksa's dialogue";
    }
}
