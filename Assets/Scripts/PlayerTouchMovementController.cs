using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTouchMovementController : MonoBehaviour
{

    public float swipeThreshold = 50f;
    public float timeThreshold = 0.3f;
    public PlayerController playerController;

    private Vector2 fingerDown;
    private Vector2 fingerUp;

    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                this.fingerDown = touch.position;
                this.fingerUp = touch.position;

                this.CheckSwipe();
            }

            if (touch.phase == TouchPhase.Stationary)
            {
                this.fingerDown = touch.position;
                this.CheckSwipe();
            }
        }
    }

    private void CheckSwipe()
    {
        /*
        float duration = (float)this.fingerUpTime.Subtract(this.fingerDownTime).TotalSeconds;
        if (duration > this.timeThreshold) return;
        */

        float deltaX = this.fingerDown.x - this.fingerUp.x;
        if (Mathf.Abs(deltaX) > this.swipeThreshold)
        {
            if (deltaX > 1)
            {
                playerController.Move(1, false);
                //Debug.Log("right");
            }

            if (deltaX < -1)
            {
                playerController.Move(-1, false);
                //Debug.Log("left");
            }
        }

        float deltaY = fingerDown.y - fingerUp.y;
        if (Mathf.Abs(deltaY) > this.swipeThreshold)
        {
            if (deltaY > 0)
            {
                //Debug.Log("up");
            }

            if (deltaY < 0)
            {
                //Debug.Log("down");
            }
        }
    }

    private void tap()
    {
        playerController.Move(0, true);
    }
}