
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Text debugText;
    public float runSpeed = 70f;

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    bool isJumping = false;
    bool jumpedLeft = false;
    bool jumpedRight = false;
    KeyCode jumpPressed = KeyCode.W;
    KeyCode left = KeyCode.A;
    KeyCode right = KeyCode.D;

    Touch playerTouch;

    private void Start()
    {
        dragDistance = Screen.height * 10 / 100; //dragDistance is 15% height of the screen
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * this section is for debugging on PC
         */
        isJumping = Input.GetKeyDown(jumpPressed);
        jumpedLeft = Input.GetKeyDown(left);
        jumpedRight = Input.GetKey(right);

        debugText.text = "jumpedRight: " + jumpedRight + " jumpedLeft: " + jumpedLeft + " isJumping: " + isJumping;

        controller.Move(runSpeed * Time.fixedDeltaTime, (jumpedRight || jumpedLeft), isJumping);


        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            Debug.Log("Right Swipe");
                            debugText.text = "Swipe right";
                            controller.Move((runSpeed * 1.2f) * Time.fixedDeltaTime, true, false);
                        }
                        else
                        {   //Left swipe
                            debugText.text = "Swipe left";
                            controller.Move((runSpeed * 1.2f) * Time.fixedDeltaTime, true, false);
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            debugText.text = "Swipe up";
                            controller.Move(0, false, true);
                        }
                        else
                        {   //Down swipe
                            debugText.text = "swipe down";
                            // Maybe implement something on down swipe if needed
                            //controller.Move(0 * Time.fixedDeltaTime, true, isJumping);
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                    controller.Move((runSpeed * 1.05f) * Time.fixedDeltaTime, true, false);
                }
            }
        }
    }

    void FixedUpdate()
    {
        
    }
}