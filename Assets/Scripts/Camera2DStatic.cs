using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DStatic : MonoBehaviour
{
    public float speed = 0f;
    public Transform player;
    public float damping = 0f;

    bool stopCamera = false;
    private Vector3 currentVelocity;
    private float cameraXPosition = 0f;

    void Start()
    {
        cameraXPosition = player.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopCamera)
        {
            Debug.Log("change");
            Vector3 targetPosition = new Vector3(cameraXPosition, speed + (speed * 0.1f));
            Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, damping);

            transform.position = newPos;
        }
    }
}
