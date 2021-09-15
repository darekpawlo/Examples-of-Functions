using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generic_Testing : MonoBehaviour
{
    [SerializeField] private Heatmap_Bool_Visual heatMapBoolVisual;
    [SerializeField] private Heatmap_Generic_Visual heatMapGenericVisual;
    private Generic_Grid<StringGridObject> stringGrid;

    private Generic_Grid<HeatMapGridObject> grid;

    private void Start()
    {
        //grid = new Generic_Grid<HeatMapGridObject>(20, 10, 5, new Vector3(-50, -25, 0), (Generic_Grid<HeatMapGridObject> g, int x, int y) => new HeatMapGridObject(g, x, y));
        stringGrid = new Generic_Grid<StringGridObject>(20, 10, 5, new Vector3(-50, -25, 0), (Generic_Grid<StringGridObject> g, int x, int y) => new StringGridObject(g, x, y));

        //heatMapGenericVisual.SetGrid(grid);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = UtilitiesClass.GetMouseWorldPosition();

        /*if (Input.GetMouseButtonDown(0))
        {
            HeatMapGridObject heatMapGridObject = grid.GetGridObject(position);
            if (heatMapGridObject != null)
            {
                heatMapGridObject.AddValue(5);
            }
        }*/

        if (Input.GetKeyDown(KeyCode.A)) { stringGrid.GetGridObject(position).AddLetter("A"); }
        if (Input.GetKeyDown(KeyCode.B)) { stringGrid.GetGridObject(position).AddLetter("B"); }
        if (Input.GetKeyDown(KeyCode.C)) { stringGrid.GetGridObject(position).AddLetter("C"); }

        if (Input.GetKeyDown(KeyCode.Alpha1)) { stringGrid.GetGridObject(position).AddNumber("1"); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { stringGrid.GetGridObject(position).AddNumber("2"); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { stringGrid.GetGridObject(position).AddNumber("3"); }
    }
}

public class HeatMapGridObject
{
    private const int MIN = 0;
    private const int MAX = 100;

    private Generic_Grid<HeatMapGridObject> grid;
    private int x;
    private int y;
    public int value;

    public HeatMapGridObject(Generic_Grid<HeatMapGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void AddValue(int addValue)
    {
        value += addValue;
        value = Mathf.Clamp(value, MIN, MAX);
        grid.TriggerGridObjectChanged(x,y);
    }

    public float GetValueNormalized()
    {
        return (float)value / MAX;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}

public class StringGridObject
{
    private Generic_Grid<StringGridObject> grid;
    private int x;
    private int y;

    private string letters;
    private string numbers;

    public StringGridObject(Generic_Grid<StringGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        letters = "";
        numbers = "";
    }

    public void AddLetter(string letter)
    {
        letters += letter;
        grid.TriggerGridObjectChanged(x, y);
    }

    public void AddNumber(string number)
    {
        numbers += number;
        grid.TriggerGridObjectChanged(x, y);
    }

    public override string ToString()
    {
        return letters + "\n" + numbers;
    }
}
