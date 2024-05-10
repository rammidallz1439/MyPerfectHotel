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
          
            Vector3 desiredPosition = camPos + target.position;

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
         
        }
    }
}

