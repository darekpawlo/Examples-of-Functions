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
        public int x;
        public int y;
        public int[,] grid;
    }

    public static Dictionary<Vector2Int, Transform> templateDictionary = new Dictionary<Vector2Int, Transform>();

    public static int[,] grid;
    [Range(1,20)]
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
        text.text = x + "," + y + "\n" + "<size=50%>" + "<color=#FFA34C>" + grid[x, y].ToString() + "</color>";

        Button button = templateTransform.transform.Find("Canvas").transform.Find("Button").GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            grid[x, y] += 1;

            OnValueChanged?.Invoke(this, new OnValueChangedEventArgs { x = x, y = y, grid = grid });
        });

        //templateDictionary[$"{x},{y}"] = templateTransform;
        templateDictionary[new Vector2Int(x,y)] = templateTransform;
    }    

    private Vector3 SpawnPosition(int x, int y)
    {
        //Moves the X by half number of rows
        //And if rows are even it moves by half of the unit size, so that it centers properly
        //Same logic for Y
        return new Vector3(x - (rows / 2 - EvenRowsorColumns(rows)), y - (columns / 2 - EvenRowsorColumns(columns)));
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

//Sprobowac zrobic dictionary, by kluczem bylo int[,], a wartoscia zwracana Transform, by nie trzeba bylo uzywac tego "transform.Find("X: " + x + " Y: " + y)"