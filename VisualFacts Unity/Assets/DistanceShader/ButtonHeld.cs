using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHeld : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public UnityEvent uEvent;
    private bool IsDown = false;
    
    void Update () {
        if (IsDown)
        {
            uEvent.Invoke();
        }
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        IsDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsDown = false;
    }
}
