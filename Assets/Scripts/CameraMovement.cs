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
        bool freecam = true;

        

        maxPos = new Vector2(3.6f, -16f);
        minPos = new Vector2(-22f, -34f);

        if (freecam)
        {
            maxPos = new Vector2(1000f, 1000f);
            minPos = - maxPos; 
        }

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
    
    public void SetCameraBoundsFloats(Vector2 max, Vector2 min)
    {
        maxPos = max;
        minPos = min;
    }
}
