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
        UpdateEveryText(e.x, e.y);
    }

    private void UpdateEveryText(int x, int y)
    {
        Vector2Int val = new Vector2Int(x, y);
        TextMeshProUGUI text = Grid_Test.templateDictionary[val].transform.Find("Canvas").transform.Find("Text").GetComponent<TextMeshProUGUI>();
        text.text = x + "," + y + "\n" + "<size=50%>" + "<color=#FFA34C>" + Grid_Test.grid[x,y].ToString() + "</color>";
    }
}
