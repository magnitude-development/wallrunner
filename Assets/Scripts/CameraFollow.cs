using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform m_player;                        // Gets player position

    private Vector3 m_offset;

    // Start is called before the first frame update
    void Start()
    {
        m_offset = transform.position - m_player.position;
    }

    private void Update()
    {
        transform.position = m_player.position + m_offset;
    }
}
