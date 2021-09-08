using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Grid_Text_Overlay_UI : MonoBehaviour
{
    private void OnEnable()
    {
        Grid_Test.OnSpawnedTile += Grid_Test_OnSpawnedTile;
    }

    private void Grid_Test_OnSpawnedTile(object sender, Grid_Test.OnSpawnedTileEveryArgs e)
    {
        UpdateText(e.x, e.y, e.grid, e.templateTransform);

        SetButtonFunction(e.x, e.y, e.grid, e.templateTransform);
    }
    private void UpdateText(int x, int y, int[,] grid, Transform templateTransform)
    {
        TextMeshProUGUI text = templateTransform.transform.Find("Canvas").transform.Find("Text").GetComponent<TextMeshProUGUI>();
        text.text = x + "," + y + "\n" + "<size=50%>" + "<color=#FFA34C>" + grid[x, y].ToString() + "</color>";
    }

    private void SetButtonFunction(int x, int y, int[,] grid, Transform templateTransform)
    {
        Button button = templateTransform.transform.Find("Canvas").transform.Find("Button").GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            grid[x, y] += 1;

            UpdateText(x, y, grid, templateTransform);
        });
    }
}
