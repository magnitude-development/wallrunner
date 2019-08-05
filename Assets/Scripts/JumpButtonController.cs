using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButtonController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private PlayerController playerController;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("iaejpmfo");
        Debug.Log(eventData.pointerPressRaycast.gameObject.name);
    }

    public virtual void OnDrag(PointerEventData data)
    {
        Debug.Log("Finger is dragging");
    }

    public virtual void OnPointerUp(PointerEventData data)
    {
        RaycastResult result = data.pointerPressRaycast;
        Debug.Log(result.gameObject.name);

        if (result.gameObject.name == "buttonHandler" || result.gameObject.name == "button" || result.gameObject.name == "JumpButton")
        {
            playerController.PlayerJump();
        }
    }
}
