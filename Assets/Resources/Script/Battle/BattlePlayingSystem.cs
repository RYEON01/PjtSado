using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayingSystem : MonoBehaviour
{
    public BattleCharacter Player { get; set; }
    public BattleCharacter Enemy { get; set; }
    public bool IsPlayerTurn { get; set; }

    public BattlePlayingSystem(BattleCharacter player, BattleCharacter enemy)
    {
        Player = player;
        Enemy = enemy;
        IsPlayerTurn = true; // Let's assume the player always goes first
    }

    // Placeholder for the method that will handle a turn
    public void HandleTurn(string playerElement, string enemyElement)
    {
        BattleCharacter attacker = IsPlayerTurn ? Player : Enemy;
        BattleCharacter defender = IsPlayerTurn ? Enemy : Player;

        int attackRoll = attacker.RollDice(playerElement);
        int defendRoll = defender.RollDice(enemyElement);

        if (attackRoll > defendRoll)
        {
            defender.HP -= (attackRoll - defendRoll);
        }

        // Switch turns
        IsPlayerTurn = !IsPlayerTurn;
    }
}
