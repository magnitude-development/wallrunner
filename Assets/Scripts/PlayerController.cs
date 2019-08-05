using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_playerSpeed = 1f;                          // Sets default player speed and gets from user input
    [SerializeField, Range(1, 100)] private float m_jumpForce = 1f;               // Sets default jumpforce and gets from user input
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private float m_currentSpeed;                              // DEBUG ONLY, REMOVE!!!
    [SerializeField, Range(1, 1000)] private float m_decimalValue;
    [SerializeField] private Joystick joystick;

    private float k_CeilingRadius = .2f;                                        // Radius of the overlap circle to determine if the player can stand up
    private float k_GroundedRadius = .2f;                                       // Radius of the overlap circle to determine if grounded
    private bool m_FacingRight = true;                                          // For determining which way the player is currently facing
    private Rigidbody2D m_player;                                               // Gets player GameObject
    private bool m_Grounded = true;                                             // Boolean for if player is on the ground
    private float buttonTimer;
    private float directionHorizontal;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GetComponent<Rigidbody2D>();
        m_decimalValue = m_decimalValue / 100;
    }

    private void Update()
    {
        CheckIfGrounded();
        directionHorizontal = joystick.Horizontal;

        if (directionHorizontal > 0)
        {
            PlayerMoveRight();
        }

        if (directionHorizontal < 0)
        {
            PlayerMoveLeft();
        }
    }

    private void CheckIfGrounded()
    {
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.name == "levelGround")
                {
                    m_Grounded = true;
                }
                else
                {
                    m_Grounded = false;
                }
            }
        } 
        else
        {
            m_Grounded = false;
        }

        
    }

    private void PlayerMoveRight()
    {
        if (m_player.velocity.x < m_playerSpeed)
        {
            m_player.AddRelativeForce(new Vector2(m_playerSpeed * Time.fixedDeltaTime, 0), ForceMode2D.Impulse);
            m_currentSpeed = m_player.velocity.x;
            if (!m_FacingRight)
            {
                Flip();
            }
        }
    }

    private void PlayerMoveLeft()
    {
        if (m_player.velocity.x > -m_playerSpeed)
        {
            m_player.AddRelativeForce(new Vector2(-(m_playerSpeed * Time.fixedDeltaTime), 0), ForceMode2D.Impulse);
            m_currentSpeed = m_player.velocity.x;
            if (m_FacingRight)
            {
                Flip();
            }
        }
    }

    public void PlayerJump()
    {
        if (m_Grounded)
        {
            if (m_player.velocity.x == 0)
            {
                m_player.AddRelativeForce(new Vector2(0, 20), ForceMode2D.Impulse);
            }
            else
            {
                m_player.AddRelativeForce(new Vector2(0, Mathf.Abs(m_player.velocity.x * (m_jumpForce / m_decimalValue))), ForceMode2D.Impulse);
            }
        }
    }

    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
