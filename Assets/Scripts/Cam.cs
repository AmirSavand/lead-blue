using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform target;

    public float smoothTime = 0.1f;
    public float distance = 10f;

    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        // No target, no update
        if (!target)
        {
            return;
        }

        // Position to smoothly move to (only on Z axis)
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, target.position.z - distance);

        // Smoothly move the camera
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
