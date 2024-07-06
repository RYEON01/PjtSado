using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera followCam;
    public CinemachineVirtualCamera zoomOutCam;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            followCam.Priority = 0;
            zoomOutCam.Priority = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            followCam.Priority = 1;
            zoomOutCam.Priority = 0;
        }
    }
}
