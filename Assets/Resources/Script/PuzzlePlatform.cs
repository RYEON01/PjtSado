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
    {
        Debug.Log("PuzzlePlatform game object: " + gameObject.name);
        if (transform.parent != null)
        {
            Debug.Log("Parent game object: " + transform.parent.gameObject.name);
            Debug.Log("Number of sibling objects: " + transform.parent.childCount);
        }
        else
        {
            Debug.Log("This game object has no parent.");
        }
    }
    
    void Update()
    {
        Debug.Log("Update method called");
        if (CheckSolution())
        {
            puzzleCollider.enabled = false;
            Debug.Log("Puzzle solved!");
        }
    }

    public void RotatePlatforms()
    {
        Debug.Log("RotatePlatforms method started");
        Debug.Log("Current rotation for platform " + gameObject.name + ": " + currentRotations[platformIndex]);
        Debug.Log("Target rotation for platform " + gameObject.name + ": " +
                  Quaternion.Euler(0f, 0f, currentRotations[platformIndex]));

        if (!hasRotated[platformIndex])
        {
            currentRotations[platformIndex] = (currentRotations[platformIndex] + 90) % 360;
            hasRotated[platformIndex] = true;
        }
        else
        {
            currentRotations[platformIndex] = (currentRotations[platformIndex] + 90) % 360;
        }

        Debug.Log("Updated rotation for platform " + gameObject.name + ": " + currentRotations[platformIndex]);
        StartCoroutine(RotateOverTime(gameObject.transform, Quaternion.Euler(0f, 0f, currentRotations[platformIndex])));
    }

    IEnumerator RotateOverTime(Transform platform, Quaternion targetRotation)
    {
        Debug.Log("RotateOverTime coroutine started for platform: " + platform.name);
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

        // Check the solution after the rotation is complete
        if (CheckSolution())
        {
            puzzleCollider.enabled = false;
            Debug.Log("Puzzle solved!");
        }
    }

    private bool CheckSolution()
    {
        Debug.Log("Checking solution");
        foreach (int rotation in currentRotations)
        {
            if (!Mathf.Approximately(rotation, 0) && !Mathf.Approximately(rotation, 360))
            {
                return false;
            }
        }

        return true;
    }
}