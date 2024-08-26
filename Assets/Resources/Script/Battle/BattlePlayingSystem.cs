using UnityEngine;

public class BattlePlayingSystem : MonoBehaviour
{
    public BattleCharacter Player { get; set; }
    public BattleCharacter Enemy { get; set; }
    public bool IsPlayerTurn { get; set; }
    public BattleDialogue BattleDialogue { get; set; }
    public Inventory Inventory { get; set; }

    public void Initialize(BattleCharacter player, BattleCharacter enemy)
    {
        Player = player;
        Player.HP = 100;
        Enemy = enemy;
        Enemy.HP = 100;
        IsPlayerTurn = true;
        BattleDialogue = new BattleDialogue(enemy);
        Inventory = new Inventory();
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
    
    public void HandlePlayerTurn()
    {
        BattleDialogue.StartDialogue();
        // TODO: Implement the attack/brace system
    }

    public void HandleEnemyTurn()
    {
        // Start the dialogue
        BattleDialogue.StartDialogue();

        // Enemy attacks the player
        EnemyAttack();
    }
    
    public void PlayerAttack()
    {
        // TODO: Implement the player's attack
    }

    public void PlayerBrace()
    {
        // TODO: Implement the player's brace
    }

    public void EnemyAttack()
    {
        // TODO: Implement the enemy's attack
    }

    public void HandleBattleEnd()
    {
        // TODO: Implement the ending phase of the battle
    }
}
