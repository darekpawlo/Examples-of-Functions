using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Grid_Test : MonoBehaviour
{
    public static event EventHandler<OnValueChangedEventArgs> OnValueChanged;
    public class OnValueChangedEventArgs : EventArgs
    {
        public int rows;
        public int columns;
        public int[,] grid;
    }

    [SerializeField] private int[,] grid;
    [SerializeField] private int rows, columns;

    private void Awake()
    {
        Transform template = transform.Find("Template");
        template.gameObject.SetActive(false);

        grid = new int[rows, columns];

        int index = 1;
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                //Assigning value to array
                grid[x, y] += index;
                SpawnTile(x, y, template);

                index++;
            }
        }
    }

    private void SpawnTile(int x, int y, Transform template)
    {
        Transform templateTransform = Instantiate(template, transform);
        templateTransform.position = SpawnPosition(x, y);
        templateTransform.gameObject.SetActive(true);

        templateTransform.name = "X: " + x + " Y: " + y;
        TextMeshProUGUI text = templateTransform.transform.Find("Canvas").transform.Find("Text").GetComponent<TextMeshProUGUI>();
        text.text = x + "," + y + "\n" + "<size=50%>" + "<color=#FFA34C>" + grid[x,y].ToString() + "</color>";

        Button button = templateTransform.transform.Find("Canvas").transform.Find("Button").GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            //value += 1;
            //grid[1, 1] += 1;
            grid[x, y] += 1;
            //text.text = x + "," + y + "\n" + "<size=50%>" + "<color=#FFA34C>" + value.ToString() + "</color>";

            OnValueChanged?.Invoke(this, new OnValueChangedEventArgs { rows = rows, columns = columns, grid = grid });
            //Debug.Log("Grid: " + grid[x, y] + " Value: " + value);
        });
    }    

    private Vector3 SpawnPosition(int x, int y)
    {
        //Moves the X by half number of rows
        //And if rows are even it moves by half of the unit size, so that it centers properly
        //Same logic for Y
        return new Vector3(x - ((rows / 2) - EvenRowsorColumns(rows)), y - ((columns / 2)) - EvenRowsorColumns(columns));
    }

    private float EvenRowsorColumns(int value)
    {        
        if (value % 2 == 1)
        {
            return 0;
        }
        else
        {
            float halfUnitSize = 0.5f;
            return halfUnitSize;
        }
    }
}
