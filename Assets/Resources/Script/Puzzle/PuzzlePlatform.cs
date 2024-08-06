using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlatform : MonoBehaviour
{
    
    public float desiredRotation = 0f; // Desired rotation in degrees
    private float rotationSpeed = 180f; // Rotation speed in degrees per second

    // Rotate the platform by a certain angle
    public IEnumerator RotatePlatform(float angle)
    {
        float targetRotation = (transform.eulerAngles.z + angle) % 360;
        while (Mathf.Abs(transform.eulerAngles.z - targetRotation) > 0.01f)
        {
            float angleToRotate = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetRotation, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, 0, angleToRotate);
            yield return null;
        }
        // Round to the nearest 90 degrees
        float roundedRotation = Mathf.Round(transform.eulerAngles.z / 90) * 90;
        transform.eulerAngles = new Vector3(0, 0, roundedRotation);
    }

    // Check if the current rotation is the desired rotation
    public bool IsSuccess()
    {
        return Mathf.Abs(transform.eulerAngles.z - desiredRotation) < 0.01f;
    }
}