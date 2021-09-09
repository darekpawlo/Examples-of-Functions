using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Grid_Button : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool clicked;

    private static Sprite sprite;

    private Image image;

    private Action action;


    private void Awake()
    {
        image = transform.Find("Image").GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        action();
    }
    public void PassFunction(Action a)
    {
        action = a;
    }

    private void ChangeSprite(Sprite s)
    {
        image.gameObject.SetActive(true);
        image.sprite = s;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!clicked)
        {
            ChangeSprite(sprite);
            image.color = new Color(image.color.r, image.color.g, image.color.b, .1f);
        }        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!clicked)
        {
            image.gameObject.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ChangeSprite(sprite);
        clicked = true;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!clicked)
        {
            image.gameObject.SetActive(false);
        }
    }

    public static void SetSprite(Sprite s)
    {
        sprite = s;
    }
}
