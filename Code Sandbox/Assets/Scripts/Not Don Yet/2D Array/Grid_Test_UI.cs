using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Grid_Test_UI : MonoBehaviour
{
    private void Start()
    {
        Grid_Test.OnValueChanged += Grid_Test_OnValueChanged;
    }

    private void Grid_Test_OnValueChanged(object sender, Grid_Test.OnValueChangedEventArgs e)
    {
        for (int x = 0; x < e.rows; x++)
        {
            for (int y = 0; y < e.columns; y++)
            {
                UpdateEveryText(x, y, e.grid[x, y]);
            }
        }
    }

    private void UpdateEveryText(int x, int y, int value)
    {
        TextMeshProUGUI text = transform.Find("X: " + x + " Y: " + y).transform.Find("Canvas").transform.Find("Text").GetComponent<TextMeshProUGUI>();
        text.text = x + "," + y + "\n" + "<size=50%>" + "<color=#FFA34C>" + value.ToString() + "</color>";
    }
}
