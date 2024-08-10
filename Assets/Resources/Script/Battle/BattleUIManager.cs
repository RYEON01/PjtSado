using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    public BattleCharacter Player { get; set; }
    public BattleCharacter Enemy { get; set; }
    public PentagonGraph PentagonGraph { get; set; }

    public BattleUIManager(BattleCharacter player, BattleCharacter enemy)
    {
        Player = player;
        Enemy = enemy;
    }

    // Placeholder for the method that will update the UI
    public void UpdateUI()
    {
        PentagonGraph.UpdateGraph(Player);
    }
    
}
