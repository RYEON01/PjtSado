using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlatform : MonoBehaviour
{
    public Collider2D puzzleCollider;
    public int platformIndex;

    private bool[] hasRotated = { false, false, false, false, false };
    private int[] currentRotations = { 180, 90, 270, 180, 0 };
    private bool[] isAtZeroDegrees = { false, false, false, false, true };

    void Start()
    {
    }

    void Update()
    {
    }

    public int GetCurrentRotation()
    {
        return currentRotations[platformIndex];
    }

    public IEnumerator RotatePlatforms()
    {
        //Debug.Log("RotatePlatforms called for platform: " + gameObject.name + " with platformIndex: " + platformIndex);

        int targetRotation = (currentRotations[platformIndex] + 90) % 360; // Calculate the target rotation

        yield return StartCoroutine(RotateOverTime(gameObject.transform, Quaternion.Euler(0f, 0f, targetRotation)));

        // Update currentRotations array after the rotation animation completes
        currentRotations[platformIndex] = Mathf.RoundToInt(gameObject.transform.eulerAngles.z / 90) * 90;

        // Update isAtZeroDegrees array
        if (currentRotations[platformIndex] == 0 || currentRotations[platformIndex] == 360)
        {
            isAtZeroDegrees[platformIndex] = true;
        }
        else
        {
            isAtZeroDegrees[platformIndex] = false;
        }

        // Print the current rotation of the platform
        //Debug.Log("Current rotation of platform: " + gameObject.name + " is " + currentRotations[platformIndex]);
    }

    IEnumerator RotateTwoPlatformsAndLog(GameObject platform1, GameObject platform2, string puzzleNumber)
    {
        Debug.Log("RotateTwoPlatformsAndLog started with puzzleNumber: " + puzzleNumber);

        PuzzlePlatform puzzlePlatform1 = platform1.GetComponent<PuzzlePlatform>();
        PuzzlePlatform puzzlePlatform2 = platform2.GetComponent<PuzzlePlatform>();

        if (puzzlePlatform1 != null)
        {
            yield return StartCoroutine(puzzlePlatform1.RotatePlatforms());
        }

        if (puzzlePlatform2 != null)
        {
            yield return StartCoroutine(puzzlePlatform2.RotatePlatforms());
        }

        Debug.Log("RotateTwoPlatformsAndLog finished with puzzleNumber: " + puzzleNumber);

        yield return null; // Add this line
    }
    
    IEnumerator RotateOverTime(Transform platform, Quaternion targetRotation)
    {
        float duration = 0.5f; // Duration of the rotation in seconds
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
}