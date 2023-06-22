using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    float startPosition = 0f;

    void Start() {
        startPosition = transform.position.x;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(Mathf.Max(target.position.x, startPosition) , transform.position.y, transform.position.z) + offset;
        // Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = desiredPosition;
    }
}
