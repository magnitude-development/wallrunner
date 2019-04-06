using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [SerializeField] private float m_wallBoost;                                 // Set value for how much player boosts up the wall
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private LayerMask m_WhatIsWall;                            // Determine what a wall is
    [SerializeField] private Transform m_WallCheck;                             // A position marking where the wall is
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching
    [SerializeField] private float m_wallCheckDistance;                         // How far we should check for a wall
    

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    private RaycastHit2D m_wallCheckHit;
    private bool isWallSliding = false;
    private bool m_isAgainstWall = false;

    // procentage is always 1.x;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

    void Update()
    {
        
        // Check if wallSlide
        if (m_wallCheckHit)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject && !isWallSliding)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
            else
            {
                m_Grounded = false;
            }
        }
        

        if (m_FacingRight)
        {
            m_isAgainstWall = Physics2D.Raycast(m_WallCheck.position, m_WallCheck.right, m_wallCheckDistance, m_WhatIsWall);
        }
        else
        {
            m_isAgainstWall = Physics2D.Raycast(m_WallCheck.position, -m_WallCheck.right, m_wallCheckDistance, m_WhatIsWall);
        }
    }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {

    }

    public void Move(float move, bool changeWall, bool jump)
    {
        /*
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }
        */
        // if player is not jumping or walljumping then just run up the wall (values for y is set in both walljump and wallboost)
        if (!jump && !changeWall)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(m_Rigidbody2D.velocity.x, move * 10f);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
        

        // If the player should jump...
        if (jump)
        {
            // Add a vertical force to the player.
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_wallBoost * 10f);
        }

        if (changeWall)
        {
            // add horizontal force to jump to other wall
            if (m_FacingRight)
            {
                Debug.Log(m_JumpForce);
                Debug.Log(move);
                Debug.Log(move + m_JumpForce);
                m_Rigidbody2D.velocity = new Vector2((move + m_JumpForce), m_Rigidbody2D.velocity.y);
            } else
            {
                m_Rigidbody2D.velocity = new Vector2(-(move + m_JumpForce), m_Rigidbody2D.velocity.y);
            }
        }

        if (m_FacingRight)
        {
            m_wallCheckHit = Physics2D.Raycast(m_WallCheck.position, m_WallCheck.right, m_wallCheckDistance, m_WhatIsWall);
            if (m_wallCheckHit)
            {
                Flip();
            }
        }
        else
        {
            m_wallCheckHit = Physics2D.Raycast(m_WallCheck.position, -m_WallCheck.right, m_wallCheckDistance, m_WhatIsWall);
            if (m_wallCheckHit)
            {
                Flip();
            }
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
