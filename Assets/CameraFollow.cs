using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;

    [Range(1, 10)]
    public float smoothFactor;

    void FixedUpdate()
    {
        var targetPostition = target.position + offset;
        var smoothedPosition = Vector3.Lerp(transform.position, targetPostition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
}
