using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float rotationSpeed = 5f;
        public Camera mainCamera;

        void Update()
        {
            // Check for mouse input
            if (Input.GetMouseButton(0))
            {
                // Get the mouse position in world space
                Vector3 mousePos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.y));

                // Calculate direction from player to mouse position
                Vector3 direction = mousePos - transform.position;
                direction.y = 0f; // Ensure the player doesn't tilt up or down

                // Move player based on direction
                transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World);

                // Rotate player towards mouse position
                RotateTowards(mousePos);
            }
        }

        void RotateTowards(Vector3 targetPosition)
        {
            // Calculate direction from player to mouse position
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0f; // Ensure the player doesn't tilt up or down

            // Smoothly rotate player towards mouse position
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

    }
}

