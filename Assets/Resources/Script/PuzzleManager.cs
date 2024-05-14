using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { get; private set; } // Singleton instance

    public List<PuzzlePlatform> puzzlePlatforms; // List of all PuzzlePlatform instances

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
        // Assume the puzzle is not solved until proven otherwise
        bool isSolved = false;

        // Check each platform's rotation
        foreach (var puzzlePlatform in puzzlePlatforms)
        {
            if (puzzlePlatform.GetCurrentRotation() == 0 || puzzlePlatform.GetCurrentRotation() == 360)
            {
                isSolved = true;
            }
            else
            {
                isSolved = false;
                break; // No need to check further, exit the loop
            }
        }

        return isSolved;
    }
}
