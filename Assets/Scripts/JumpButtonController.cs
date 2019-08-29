using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private PlayerController playerController;

    private bool m_holdingDown = false;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        RaycastResult result = eventData.pointerPressRaycast;
        Debug.Log(result.gameObject.name);

        if (result.gameObject.name == "buttonHandler" || result.gameObject.name == "button" || result.gameObject.name == "JumpButton")
        {
            m_holdingDown = true;
        }
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        m_holdingDown = false;
    }

    void Update()
    {
        if (m_holdingDown)
        {
            playerController.HoldingJump();
            playerController.PlayerJump();
        }
        else
        {
            playerController.NotHoldingJump();
        }
    }
}
