using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heatmap_Testing : MonoBehaviour
{
    [SerializeField] private Heatmap_Visual heatmap_Visual;
    private Heatmap_Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Heatmap_Grid(100, 100, 1f, Vector3.zero);

        heatmap_Visual.SetGrid(grid);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = UtilitiesClass.GetMouseWorldPosition();
            grid.AddValue(position, 25, 2, 10);
        }
    }
}
