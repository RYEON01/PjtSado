using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlatform : MonoBehaviour
{
    public Collider2D puzzleCollider;
    public int platformIndex;

    private bool[] hasRotated = { false, false, false, false, false };
    private int[] currentRotations = { 180, 90, 270, 180, 0 };

    void Start()
    {/*
        if (transform.parent != null)
        {
            Debug.Log("Parent game object: " + transform.parent.gameObject.name);
            Debug.Log("Number of sibling objects: " + transform.parent.childCount);
        }
        else
        {
            Debug.Log("This game object has no parent.");
        }
        */
    }
    
    void Update()
    {
        //Debug.Log("Update method called");
        if (CheckSolution())
        {
            puzzleCollider.enabled = false;
            //Debug.Log("Puzzle solved!");
        }
        // If the puzzle is solved, do not allow further rotations
        if (CheckSolution())
        {
            //Debug.Log("Puzzle solved! No further rotations allowed.");
            return;
        }
    }

    public void RotatePlatforms()
    {
        
        // If the puzzle is solved, do not allow further rotations
        if (CheckSolution())
        {
            //Debug.Log("Puzzle solved! No further rotations allowed.");
            return;
        }
        
        //Debug.Log("RotatePlatforms method started");
        //Debug.Log("Current rotation for platform " + gameObject.name + ": " + currentRotations[platformIndex]);
        //Debug.Log("Target rotation for platform " + gameObject.name + ": " +
                  Quaternion.Euler(0f, 0f, currentRotations[platformIndex]);

        if (!hasRotated[platformIndex])
        {
            currentRotations[platformIndex] = (currentRotations[platformIndex] + 90) % 360;
            hasRotated[platformIndex] = true;
        }
        else
        {
            currentRotations[platformIndex] = (currentRotations[platformIndex] + 90) % 360;
        }

        //Debug.Log("Updated rotation for platform " + gameObject.name + ": " + currentRotations[platformIndex]);
        StartCoroutine(RotateOverTime(gameObject.transform, Quaternion.Euler(0f, 0f, currentRotations[platformIndex])));

        // Print out the currentRotations array after each rotation
        //Debug.Log("Current rotations after rotation: " + string.Join(", ", currentRotations));
        
    }

    IEnumerator RotateOverTime(Transform platform, Quaternion targetRotation)
    {
        //Debug.Log("RotateOverTime coroutine started for platform: " + platform.name);
        float duration = 1.0f; // Duration of the rotation in seconds
        Quaternion startRotation = platform.rotation;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            platform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;

            yield return null;
        }

        platform.rotation = targetRotation;
    }

    public bool CheckSolution()
    {
        Debug.Log("Checking solution");

        // Define the desired state
        int[] desiredRotations = { 180, 0, 270, 180, 0 };

        // Print out the currentRotations and desiredRotations arrays
        Debug.Log("Current rotations: " + string.Join(", ", currentRotations));
        Debug.Log("Desired rotations: " + string.Join(", ", desiredRotations));

        // Assume the puzzle is solved until proven otherwise
        bool isSolved = true;

        // Check each platform's rotation
        for (int i = 0; i < currentRotations.Length; i++)
        {
            // If the current rotation does not match the desired rotation, the puzzle is not solved
            if (currentRotations[i] != desiredRotations[i])
            {
                isSolved = false;
                break; // No need to check further, exit the loop
            }
        }

        Debug.Log("Solution check result: " + isSolved);
        //Debug.Log("Puzzle collider enabled state before check: " + puzzleCollider.enabled);
        
        if (isSolved)
        {
            // If the puzzle is solved, set isColliding to false in the PlayerMove script
            PlayerMove playerMove = FindObjectOfType<PlayerMove>();
            if (playerMove != null)
            {
                playerMove.isColliding = false;
                playerMove.collidedObject = null;
            }
        }
        
        return isSolved;
    }
}