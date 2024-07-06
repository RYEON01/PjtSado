using System.Collections;
using System.Linq;
using UnityEngine;

public class PuzzlePlatformMover : MonoBehaviour
{
    public Vector3[] positions; // Array of positions
    public int[] desiredPositionIndices; // Indices of the desired positions in the positions array
    private int currentPositionIndex = 0; // Index of the current position in the positions array
    private float moveSpeed = 10f; // Speed at which the platform moves

    // Move the platform to the next position in the sequence
    public IEnumerator MovePlatform()
    {
        // Calculate the next position index
        int nextPositionIndex = (currentPositionIndex + 1) % positions.Length;

        Debug.Log("Moving platform to position: " + positions[nextPositionIndex]);

        // Move the platform to the next position
        while (Vector3.Distance(transform.localPosition, positions[nextPositionIndex]) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, positions[nextPositionIndex], moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Update the current position index
        currentPositionIndex = nextPositionIndex;

        Debug.Log("Platform moved to position: " + transform.localPosition);
    }

    // Check if the current position index is one of the desired position indices
    public bool IsSuccess()
    {
        return desiredPositionIndices.Contains(currentPositionIndex);
    }
}