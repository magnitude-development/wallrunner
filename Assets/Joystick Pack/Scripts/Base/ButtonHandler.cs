using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private PlayerController playerController;
    private Canvas canvas;

    protected virtual void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
            Debug.LogError("The Joystick is not placed inside a canvas");
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        playerController.PlayerJump(eventData.);
    }
}