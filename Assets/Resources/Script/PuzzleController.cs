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

    public void HandlePuzzleInteraction()
    {
        if (puzzlePlatform1 != null && puzzlePlatform3 != null)
        {
            RotateAndLog(puzzlePlatform1, "1");
            RotateAndLog(puzzlePlatform3, "3");
        }
        if (puzzlePlatform2 != null && puzzlePlatform4 != null)
        {
            RotateAndLog(puzzlePlatform2, "2");
            RotateAndLog(puzzlePlatform4, "4");
        }
        if (puzzlePlatform3 != null && puzzlePlatform5 != null)
        {
            RotateAndLog(puzzlePlatform3, "3");
            RotateAndLog(puzzlePlatform5, "5");
        }
        if (puzzlePlatform1 != null && puzzlePlatform4 != null)
        {
            RotateAndLog(puzzlePlatform1, "1");
            RotateAndLog(puzzlePlatform4, "4");
        }
        if (puzzlePlatform2 != null && puzzlePlatform5 != null)
        {
            RotateAndLog(puzzlePlatform2, "2");
            RotateAndLog(puzzlePlatform5, "5");
        }
    }
    
    void RotateAndLog(GameObject platform, string puzzleNumber)
    {
        Debug.Log("RotateAndLog 함수 실행됐다!");
        PuzzlePlatform puzzlePlatform = platform.GetComponent<PuzzlePlatform>();
        
        if (puzzlePlatform != null)
        {
            puzzlePlatform.RotatePlatforms();
            Debug.Log(puzzleNumber + "RotatePlatforms 불러왔다!");
        }
    }
    
}
