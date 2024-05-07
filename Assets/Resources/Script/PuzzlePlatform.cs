using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlatform : MonoBehaviour
{
    public Collider2D puzzleCollider;

    private Quaternion[] targetRotations = {
        Quaternion.Euler(0f, 0f, 0f),
        Quaternion.Euler(0f, 0f, 90f),
        Quaternion.Euler(0f, 0f, 270f),
        Quaternion.Euler(0f, 0f, 180f),
        Quaternion.Euler(0f, 0f, 0f)
    };

    private int[] currentRotations = {180, 90, 270, 180, 0};

    public void RotatePlatforms()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform platform = transform.GetChild(i);
            platform.rotation = Quaternion.Euler(0f, 0f, currentRotations[i]);
            currentRotations[i] = (currentRotations[i] + 90) % 360;
        }

        if (CheckSolution())
        {
            puzzleCollider.enabled = false;
            Debug.Log("Puzzle solved!");
        }
    }

    private bool CheckSolution()
    {
        foreach (int rotation in currentRotations)
        {
            if (rotation != 0)
            {
                return false;
            }
        }
        return true;
    }
}
