using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Grid_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image image;

    public static Sprite sprite;

    private bool clicked;
    private bool gameEnded;

    private void OnEnable()
    {
        Grid_Tick_Tack_Toe.OnGameEnded += Grid_Tick_Tack_Toe_OnGameOver;
    }

    private void OnDisable()
    {
        Grid_Tick_Tack_Toe.OnGameEnded -= Grid_Tick_Tack_Toe_OnGameOver;
    }

    private void Grid_Tick_Tack_Toe_OnGameOver(object sender, EventArgs e)
    {
        gameEnded = true;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
    }

    private void Start()
    {
        image = transform.Find("Image").GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!clicked && !gameEnded)
        {
            image.gameObject.SetActive(true);
            image.sprite = sprite;
            image.color = new Color(image.color.r, image.color.g, image.color.b, .1f);
        }        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!clicked && !gameEnded)
        {
            image.gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!clicked && !gameEnded)
        {
            clicked = true;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        }        
    }
}
