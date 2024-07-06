using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManagerMover : MonoBehaviour
{
    public static PuzzleManagerMover Instance { get; private set; } // Singleton instance
    public PuzzlePlatformMover[] platformMovers; // Array of all PuzzlePlatformMover instances
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

        foreach (var platformMover in platformMovers)
        {
            if (!platformMover.IsSuccess())
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