using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tutorial link
//https://www.youtube.com/watch?v=7Y7RDRMx4NQ

public class GridManager : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private float[,] grid;
    private int vertical, horizontal, columns, rows;

    private void Start()
    {
        //By default equals 5
        vertical = (int)Camera.main.orthographicSize;
        //equals = 5 * (1024 / 512)
        //doesn't work too well if the value doesn't return as int
        horizontal = vertical * (Screen.width / Screen.height);

        columns = horizontal * 2;
        rows = vertical * 2;

        grid = new float[columns, rows];

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                grid[i, j] = Random.Range(0.0f, 1.0f);
                SpawnTile(i, j, grid[i, j]);
            }
        }
    }

    private void SpawnTile(int x, int y, float value)
    {
        GameObject g = new GameObject("X: " + x + " Y: " + y);
        //Using only X and Y it would start from 0,0
        float unitSize = .5f;
        g.transform.position = new Vector3(x - (horizontal - unitSize), y - (vertical - unitSize));
        var s = g.AddComponent<SpriteRenderer>();
        s.color = new Color(value, value, value);
        s.sprite = sprite;
    }
}
