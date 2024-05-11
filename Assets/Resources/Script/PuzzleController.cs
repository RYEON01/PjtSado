using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public GameObject puzzlePlatform1;
    public GameObject puzzlePlatform2;
    public GameObject puzzlePlatform3;
    public GameObject puzzlePlatform4;
    public GameObject puzzlePlatform5;

    
    void Start()
    {
        AssignChildrenToPuzzlePlatforms();
    }

    void AssignChildrenToPuzzlePlatforms()
    {
        puzzlePlatform1 = GameObject.Find("Platform_A").transform.parent.gameObject;
        puzzlePlatform2 = GameObject.Find("Platform_B").transform.parent.gameObject;
        puzzlePlatform3 = GameObject.Find("Platform_C").transform.parent.gameObject;
        puzzlePlatform4 = GameObject.Find("Platform_D").transform.parent.gameObject;
        puzzlePlatform5 = GameObject.Find("Platform_E").transform.parent.gameObject;

        /*
        Debug.Log("PuzzlePlatform1 game object: " + puzzlePlatform1.name + ", child objects: " + puzzlePlatform1.transform.childCount);
        Debug.Log("PuzzlePlatform2 game object: " + puzzlePlatform2.name + ", child objects: " + puzzlePlatform2.transform.childCount);
        Debug.Log("PuzzlePlatform3 game object: " + puzzlePlatform3.name + ", child objects: " + puzzlePlatform3.transform.childCount);
        Debug.Log("PuzzlePlatform4 game object: " + puzzlePlatform4.name + ", child objects: " + puzzlePlatform4.transform.childCount);
        Debug.Log("PuzzlePlatform5 game object: " + puzzlePlatform5.name + ", child objects: " + puzzlePlatform5.transform.childCount);
        */
    }
    public void HandlePuzzleInteraction(string tag)
    {
        switch (tag)
        {
            case "PuzzleTrigger1":
                RotateAndLog(puzzlePlatform1, "1");
                RotateAndLog(puzzlePlatform3, "3");
                break;
            case "PuzzleTrigger2":
                RotateAndLog(puzzlePlatform2, "2");
                RotateAndLog(puzzlePlatform4, "4");
                break;
            case "PuzzleTrigger3":
                RotateAndLog(puzzlePlatform3, "3");
                RotateAndLog(puzzlePlatform5, "5");
                break;
            case "PuzzleTrigger4":
                RotateAndLog(puzzlePlatform1, "1");
                RotateAndLog(puzzlePlatform4, "4");
                break;
            case "PuzzleTrigger5":
                RotateAndLog(puzzlePlatform2, "2");
                RotateAndLog(puzzlePlatform5, "5");
                break;
        }
    }

    
    void RotateAndLog(GameObject platform, string puzzleNumber)
    {
        //Debug.Log("RotateAndLog 함수 실행됐다!");
        //Debug.Log("Number of child objects: " + platform.transform.childCount);
        for (int i = 0; i < platform.transform.childCount; i++)
        {
            PuzzlePlatform puzzlePlatform = platform.transform.GetChild(i).GetComponent<PuzzlePlatform>();

            if (puzzlePlatform != null)
            {
                //Debug.Log("PuzzlePlatform component found on child object: " + platform.transform.GetChild(i).name);
                puzzlePlatform.RotatePlatforms();
                //Debug.Log(puzzleNumber + " RotatePlatforms called!");
            }
            else
            {
                //Debug.Log("Child object: " + platform.transform.GetChild(i).name + " does not have a PuzzlePlatform component.");
            }
        }
    }
    
}
