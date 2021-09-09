using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class mButton : MonoBehaviour, IPointerClickHandler
{
    private Action action;

    public void OnPointerClick(PointerEventData eventData)
    {
        action();
    }

    public void AddListener(Action a)
    {
        action = a;
    }
}
