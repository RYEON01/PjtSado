using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PuzzleController : MonoBehaviour
{
    
    public PuzzlePlatform platform1;
    public PuzzlePlatform platform2;
    private bool isInteracting = false;

    public IEnumerator HandleInteraction()
    {
        isInteracting = true;
        yield return StartCoroutine(platform1.RotatePlatform(90f));
        yield return StartCoroutine(platform2.RotatePlatform(90f));
        PuzzleManager.Instance.CheckSolution();
        isInteracting = false;
    }

    public bool IsInteracting()
    {
        return isInteracting;
    }
}
