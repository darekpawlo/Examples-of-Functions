using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Test : MonoBehaviour
{
    private void Awake()
    {
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {

        });

        transform.Find("EdgeScrollingToggle").GetComponent<Toggle>().SetIsOnWithoutNotify(true);
    }
}
