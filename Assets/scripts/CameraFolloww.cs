using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraFolloww : MonoBehaviour
{
    public Transform Target; // The player's transform

    public float CameraSpeed; // Time it takes for the camera to move to the target position


    void FixedUpdate()
    {
        if (Target!=null)
        {
            Vector2 targetPosition = Vector2.Lerp(transform.position, Target.position, Time.deltaTime * CameraSpeed);
            transform.position = new Vector3(targetPosition.x, 0, -10);
        }
    }
}

