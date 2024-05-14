using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PuzzleController : MonoBehaviour
{
    public GameObject puzzlePlatform1;
    public GameObject puzzlePlatform2;
    public GameObject puzzlePlatform3;
    public GameObject puzzlePlatform4;
    public GameObject puzzlePlatform5;

    public List<PuzzlePlatform> puzzlePlatforms = new List<PuzzlePlatform>();
    
    void Start()
    {
        AssignChildrenToPuzzlePlatforms();
    }

    void AssignChildrenToPuzzlePlatforms()
    {
        puzzlePlatform1 = GameObject.Find("Platform_A");
        puzzlePlatform2 = GameObject.Find("Platform_B");
        puzzlePlatform3 = GameObject.Find("Platform_C");
        puzzlePlatform4 = GameObject.Find("Platform_D");
        puzzlePlatform5 = GameObject.Find("Platform_E");

        // Add the PuzzlePlatform components of the GameObjects to the puzzlePlatforms list
        puzzlePlatforms.Add(puzzlePlatform1.GetComponent<PuzzlePlatform>());
        puzzlePlatforms.Add(puzzlePlatform2.GetComponent<PuzzlePlatform>());
        puzzlePlatforms.Add(puzzlePlatform3.GetComponent<PuzzlePlatform>());
        puzzlePlatforms.Add(puzzlePlatform4.GetComponent<PuzzlePlatform>());
        puzzlePlatforms.Add(puzzlePlatform5.GetComponent<PuzzlePlatform>());
    }

    public IEnumerator HandlePuzzleInteraction(string tag)
    {
        Debug.Log("HandlePuzzleInteraction started with tag: " + tag);
        switch (tag)
        {
            case "PuzzleTrigger1":
                yield return StartCoroutine(RotateTwoPlatformsAndLog(puzzlePlatform1, puzzlePlatform3, "1"));
                break;
            case "PuzzleTrigger2":
                yield return StartCoroutine(RotateTwoPlatformsAndLog(puzzlePlatform2, puzzlePlatform4, "2"));
                break;
            case "PuzzleTrigger3":
                yield return StartCoroutine(RotateTwoPlatformsAndLog(puzzlePlatform3, puzzlePlatform5, "3"));
                break;
            case "PuzzleTrigger4":
                yield return StartCoroutine(RotateTwoPlatformsAndLog(puzzlePlatform1, puzzlePlatform4, "4"));
                break;
            case "PuzzleTrigger5":
                yield return StartCoroutine(RotateTwoPlatformsAndLog(puzzlePlatform2, puzzlePlatform5, "5"));
                break;
        }
        Debug.Log("2HandlePuzzleInteraction finished with tag: " + tag);

        // Print the current rotation of all platforms
        foreach (var puzzlePlatform in puzzlePlatforms)
        {
            Debug.Log("Current rotation of platform: " + puzzlePlatform.gameObject.name + " is " + puzzlePlatform.GetCurrentRotation());
        }

        // Check the solution after handling the interaction
        if (PuzzleManager.Instance.CheckSolution())
        {
            Debug.Log("Puzzle solved! No further interactions allowed.");
        }
        else
        {
            Debug.Log("Puzzle not solved yet. Further interactions allowed.");
        }
        Debug.Log("1HandlePuzzleInteraction finished with tag: " + tag);
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
    }
}
