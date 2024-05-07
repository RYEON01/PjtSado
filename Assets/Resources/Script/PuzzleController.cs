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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PuzzleTrigger1"))
        {
            Interact(puzzlePlatform1);
        }
        else if (other.CompareTag("PuzzleTrigger2"))
        {
            Interact(puzzlePlatform2);
        }
        else if (other.CompareTag("PuzzleTrigger3"))
        {
            Interact(puzzlePlatform3);
        }
        else if (other.CompareTag("PuzzleTrigger4"))
        {
            Interact(puzzlePlatform4);
        }
        else if (other.CompareTag("PuzzleTrigger5"))
        {
            Interact(puzzlePlatform5);
        }
    }

    void Interact(GameObject platform)
    {
        PuzzlePlatform puzzlePlatform = platform.GetComponent<PuzzlePlatform>();
        if (puzzlePlatform != null)
        {
            puzzlePlatform.RotatePlatforms();
        }
    }
}
