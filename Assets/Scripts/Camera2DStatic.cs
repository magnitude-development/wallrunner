using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DStatic : MonoBehaviour
{
    public Transform player;
    public float damping = 0f;

    bool stopCamera = false;
    private Vector3 currentVelocity;

    // Update is called once per frame
    void Update()
    {
        if (!stopCamera)
        {
            Vector3 targetPosition = new Vector3(0, player.transform.position.y + (player.transform.position.y * 0.1f));
            Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, damping);

            transform.position = newPos;
        }
    }
}
