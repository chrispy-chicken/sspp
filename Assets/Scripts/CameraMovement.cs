using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // target the player
    public float smoothing; // camera speed
    private Vector2 maxPos;
    private Vector2 minPos;
    private Vector3 targetPosition;

    void Start()
    {
        // with different areas this might need to be adjusted with a lookup table or something
        maxPos = new Vector2(5.5f, 2f);
        minPos = - maxPos; 
    }
    void LateUpdate()
    {
        targetPosition = new Vector3(target.position.x, target.position.y, 0f) + new Vector3(0f, 0f, -10f); // adjust for z axis

        targetPosition.x = Mathf.Clamp(targetPosition.x, minPos.x, maxPos.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minPos.y, maxPos.y);

        if (transform.position != targetPosition)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }

    }
}
