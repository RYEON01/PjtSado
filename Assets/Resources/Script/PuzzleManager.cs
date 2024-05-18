using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    
    public static PuzzleManager Instance { get; private set; } // Singleton instance
    public PuzzlePlatform[] platforms; // Array of all PuzzlePlatform instances
    public GameObject PuzzleCollider;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool CheckSolution()
    {
        bool isSolved = true; // Assume the puzzle is solved until proven otherwise

        foreach (var platform in platforms)
        {
            if (!platform.IsSuccess())
            {
                isSolved = false; // Puzzle is not solved
                Debug.Log("Puzzle not solved yet. Further interactions allowed.");
                break;
            }
        }

        if (isSolved)
        {
            Debug.Log("Puzzle solved! No further interactions allowed.");
            PuzzleCollider.SetActive(false); // Disable the PuzzleCollider
        }

        return isSolved;
    }
}
