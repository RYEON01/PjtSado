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
    
    public void ShowDialogue(string dialogue)
    {
        // TODO: Show the dialogue on the UI
    }

    public void ShowAnswerChoices(string[] choices)
    {
        // TODO: Show the answer choices on the UI
    }
    
}
