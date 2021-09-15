using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Grid_Text_Overlay_UI : MonoBehaviour
{
    [SerializeField] private bool addValue;

    private void OnEnable()
    {
        Grid_Test.OnSpawnedTile += Grid_Test_OnSpawnedTile;
    }

    private void OnDisable()
    {
        Grid_Test.OnSpawnedTile -= Grid_Test_OnSpawnedTile;
    }

    private void Grid_Test_OnSpawnedTile(object sender, Grid_Test.OnSpawnedTileEveryArgs e)
    {
        UpdateGridPieceText(e.x, e.y, e.grid, e.templateTransform);

        SetGridPieceButtonFunction(e.x, e.y, e.grid, e.templateTransform);
    }

    private void UpdateGridPieceText(int x, int y, int[,] grid, Transform templateTransform)
    {
        TMP_Text text = templateTransform.transform.Find("Canvas").transform.Find("Text").GetComponent<TMP_Text>();
        text.text = x + "," + y + "\n" + "<size=50%>" + "<color=#FFA34C>" + grid[x, y].ToString() + "</color>";
    }

    private void SetGridPieceButtonFunction(int x, int y, int[,] grid, Transform templateTransform)
    {
        Button button = templateTransform.transform.Find("Canvas").transform.Find("Button").GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (addValue)
            {
                grid[x, y] += 1;
            }

            UpdateGridPieceText(x, y, grid, templateTransform);
        });
    }
}
