using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform;

    public Vector3 offset;

    public Vector2 maxPosition;

    public Vector3 minPosition;

    public float interpolationRatio = 0.5f;

    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;

    }

    void FixedUpdate()
    {
        /*if(playerTransform != null)
        {
            Vector3 desiredPosition = playerTransform.position + offset;

            float clampX = Mathf.Clamp(desiredPosition.x, minPosition.x, maxPosition.x);
            float clampY = Mathf.Clamp(desiredPosition.y, minPosition.y, maxPosition.y);
            Vector3 clampedPosition = new Vector3(clampX, clampY, desiredPosition.z);

            Vector3 lerpedPosition = Vector3.Lerp(transform.position, clampedPosition, interpolationRatio);

            transform.position = lerpedPosition;
        }*/

        if(playerTransform == null)
        {
            return;
        }
        Vector3 desiredPosition = playerTransform.position + offset;

        float clampX = Mathf.Clamp(desiredPosition.x, minPosition.x, maxPosition.x);
        float clampY = Mathf.Clamp(desiredPosition.y, minPosition.y, maxPosition.y);
        Vector3 clampedPosition = new Vector3(clampX, clampY, desiredPosition.z);

        Vector3 lerpedPosition = Vector3.Lerp(transform.position, clampedPosition, interpolationRatio);

        transform.position = lerpedPosition;
        
    }
}
