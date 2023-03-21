using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Transform lookAt;

    [Header("Tweaks")]  
    [SerializeField] private Vector3 offset = new Vector3(0, 10, -10);

    [Header("Bounds")]
    [SerializeField] private bool enabledBoundX = true;
    [SerializeField] private float boundX = 1.0f;

    [Header("Smooth")]
    [SerializeField] private bool smooth = true;
    [SerializeField] private float smoothSpeed = 5;

    [Header("Fields")]
    private Vector3 desiredPosition;

    // LateUpdate, as we have to make sure the camera moves AFTER the player
    private void LateUpdate()
    {
        desiredPosition = lookAt.position + offset;

        if (enabledBoundX)
        {
            // This is to check if we're inside the bounds on the X axis
            float deltaX = lookAt.position.x - transform.position.x;
            if (Mathf.Abs(deltaX) > boundX)
            {
                if (deltaX > 0)
                {
                    desiredPosition.x = lookAt.position.x - boundX;
                }
                else
                {
                    desiredPosition.x = lookAt.position.x + boundX;
                }
            }
            else
            {
                desiredPosition.x = lookAt.position.x - deltaX;
            }
        }

        if (smooth)
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        else
            transform.position = desiredPosition;
    }
}
