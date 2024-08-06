using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDialogue : MonoBehaviour
{
    public BattleCharacter Enemy { get; set; }
    public int Compassion { get; set; }

    public BattleDialogue(BattleCharacter enemy)
    {
        Enemy = enemy;
        Compassion = 0;
    }

    // Placeholder for the method that will handle a dialogue turn
    public void HandleDialogueTurn(string playerResponse)
    {
        // TODO: Adjust the enemy's compassion level based on the player's response
    }
}
