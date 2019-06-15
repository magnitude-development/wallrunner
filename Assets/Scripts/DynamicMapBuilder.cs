using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

public class DynamicMapBuilder : MonoBehaviour
{
    // Unity public variables
    [SerializeField] GameObject m_wallGraphicGameObj;                       // Get sprite gameobject to add to scene
    [SerializeField] Sprite m_wallSprite;                                   // Get sprite for wall
    [SerializeField] Camera m_mainCamera;                                   // Get camera reference to check if sprites are on screen

    // Private variables
    private SpriteRenderer m_wall;                                          // Initiate variable for wall image for later use
    private bool firstLeft = true;                                          // Set to know if it's first time render left side
    private bool firstRight = true;                                         // Set to know if it's first time render right side
    private Vector2 m_previousLeftBox;                                      // Last position of added piece of wall leftside
    private Vector2 m_previousRightBox;                                     // Last position of added piece of wall rightside
    private BoxCollider2D m_boxCollider;                                    // Set boxcollider to each wall piece
    private int m_cameraRectSize;                                            // Get camera rectangle size

    // Start is called before the first frame update
    void Start()
    {
        m_cameraRectSize = m_mainCamera.pixelWidth;
    }

    // FixedUpdate is run every 0.2s which is set in projectsettings
    void FixedUpdate()
    {
        addObjectToSceneLeft();
        addObjectToSceneRight();
    }

    void addObjectToSceneLeft()
    {
        // Initiate new instance of wall GameObject and set parameters
        m_wallGraphicGameObj = new GameObject();
        m_wallGraphicGameObj.name = "Wall";
        m_wallGraphicGameObj.AddComponent<SpriteRenderer>();
        m_wallGraphicGameObj.AddComponent<BoxCollider2D>();

        // Layer 10 is layer called "Level"
        m_wallGraphicGameObj.layer = 10;


        // Initiate new instance of wall canvas and set parameters
        m_wall = m_wallGraphicGameObj.GetComponent<SpriteRenderer>();
        m_wall.sprite = m_wallSprite;

        // Set collider for each wall piece which player check for with a raycast to determine if it has reached the wall or not
        m_boxCollider = m_wallGraphicGameObj.GetComponent<BoxCollider2D>();
        m_boxCollider.size = new Vector2(m_wall.size.x, m_wall.size.y);

        // do this to put first block att the bottom or else it will have a gap at the bottom
        if (firstLeft)
        {
            m_wall.transform.position = new Vector2(-(Mathf.Ceil(m_cameraRectSize) - m_mainCamera.transform.position.x), 0);
            firstLeft = false;
        }
        else
        {
            if (m_wall.transform.position.y < m_mainCamera.transform.position.y) 
            {
                m_wall.transform.position = new Vector2(-(Mathf.Ceil(m_cameraRectSize) - m_mainCamera.transform.position.x), (m_wall.size.y * m_wall.transform.localScale.y) + m_previousLeftBox.y);
                m_previousLeftBox = m_wall.transform.position;
            }
        }

        // This does not work, it should check for if the position is lower than the hight of the view of camera but needs to be fixed
        if (m_wall.transform.position.y < m_mainCamera.transform.position.y)
        {
            SceneManager.MoveGameObjectToScene(m_wallGraphicGameObj, SceneManager.GetActiveScene());
        }
    }

    void addObjectToSceneRight()
    {
        // Initiate new instance of wall GameObject and set parameters
        m_wallGraphicGameObj = new GameObject();
        m_wallGraphicGameObj.name = "Wall";
        m_wallGraphicGameObj.AddComponent<SpriteRenderer>();
        m_wallGraphicGameObj.AddComponent<BoxCollider2D>();

        // Layer 10 is layer called "Level"
        m_wallGraphicGameObj.layer = 10;

        // Initiate new instance of wall canvas and set parameters
        m_wall = m_wallGraphicGameObj.GetComponent<SpriteRenderer>();
        m_wall.sprite = m_wallSprite;

        // Set collider for each wall piece which player check for with a raycast to determine if it has reached the wall or not
        m_boxCollider = m_wallGraphicGameObj.GetComponent<BoxCollider2D>();
        m_boxCollider.size = new Vector2(m_wall.size.x, m_wall.size.y);

        if (firstRight)
        {
            m_wall.transform.position = new Vector2(Mathf.Ceil(m_cameraRectSize) - m_mainCamera.transform.position.x, 0);
            firstRight = false;
        }
        else
        {
            Debug.Log(m_mainCamera.rect.x);
            Debug.Log(m_mainCamera.transform.position.x);
            if (m_wall.transform.position.y < m_mainCamera.transform.position.y)
            {
                m_wall.transform.position = new Vector2(Mathf.Ceil(m_cameraRectSize) - m_mainCamera.transform.position.x, (m_wall.size.y * m_wall.transform.localScale.y) + m_previousRightBox.y);
                m_previousRightBox = m_wall.transform.position;
            }
        }

        // This does not work, it should check for if the position is lower than the hight of the view of camera but needs to be fixed
        if (m_wall.transform.position.y < m_mainCamera.transform.position.y)
        {
            // SceneManager.MoveGameObjectToScene(m_wallGraphicGameObj, SceneManager.GetActiveScene());
        }
    }

    // TODO: ADD DELETE OF BLOCKS OUTSIDE THE CAMERA VIEW
}
