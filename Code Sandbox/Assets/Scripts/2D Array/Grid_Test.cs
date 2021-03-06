using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Grid_Test : MonoBehaviour
{
    public static Dictionary<Vector2, Transform> templatesDictionary = new Dictionary<Vector2, Transform>();

    public static event EventHandler<OnSpawnedGirdEventArgs> OnSpawnedGrid;
    public class OnSpawnedGirdEventArgs : EventArgs
    {
        public int[,] grid;
    }
    public static event EventHandler<OnSpawnedTileEveryArgs> OnSpawnedTile;
    public class OnSpawnedTileEveryArgs: EventArgs
    {
        public Transform templateTransform;
        public int x;
        public int y;
        public int[,] grid;
    }

    private int[,] grid;
    [Range(1,20)]
    [SerializeField] private int rows, columns;

    private void Start()
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
                //grid[x, y] += index;
                SpawnTile(x, y, template);

                index++;
            }
        }

        OnSpawnedGrid?.Invoke(this, new OnSpawnedGirdEventArgs { grid = grid });
    }

    private void SpawnTile(int x, int y, Transform template)
    {
        Transform templateTransform = Instantiate(template, transform);
        templateTransform.position = SpawnPosition(x, y);
        templateTransform.name = "X: " + x + " Y: " + y;
        templateTransform.gameObject.SetActive(true);

        OnSpawnedTile?.Invoke(this, new OnSpawnedTileEveryArgs { templateTransform = templateTransform, x = x, y = y, grid = grid });
        templatesDictionary[new Vector2(x, y)] = templateTransform;
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