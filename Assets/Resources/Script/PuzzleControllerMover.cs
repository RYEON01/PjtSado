using System.Collections;
using UnityEngine;

public class PuzzleControllerMover : MonoBehaviour
{
    public PuzzlePlatformMover platform1;
    public PuzzlePlatformMover platform2;
    private bool isInteracting = false;

    public IEnumerator HandleInteraction()
    {
        Debug.Log("HandleInteraction called");
        isInteracting = true;
        yield return StartCoroutine(platform1.MovePlatform());
        yield return StartCoroutine(platform2.MovePlatform());
        PuzzleManagerMover.Instance.CheckSolution();
        isInteracting = false;
    }

    public bool IsInteracting()
    {
        return isInteracting;
    }
}