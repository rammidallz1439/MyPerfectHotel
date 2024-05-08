using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 camPos;
    public Transform target; // Player's transform
    public float smoothSpeed = 0.125f; // Smoothing factor

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate desired position for the camera
            Vector3 desiredPosition = camPos + target.position;

            // Smoothly move the camera towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Make the camera always look at the player
            //transform.LookAt(target);
        }
    }
}

