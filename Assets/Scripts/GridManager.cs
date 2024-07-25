using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Collections;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] int rows;
    [SerializeField] int columns;
    [SerializeField] float cellSize;

    Color controlledColor = Color.red;

    [SerializeField] GameObject cellPrefab;
    GameObject[,] gridArray;

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid(GameObject.Find("GridHolder"));
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) UpdateGrid();
    }

    void CreateGrid(GameObject parent) {
        gridArray = new GameObject[rows, columns];

        List<int> possibleValues = new List<int> {0,1,2};

        WaveFunctionCollapse wfc = new WaveFunctionCollapse(rows, columns, possibleValues);
        int[,] wfcGrid = wfc.GenerateGrid();

        int halfRows = rows / 2;
        int halfColumns = columns / 2;

        for (int x = -halfRows; x < halfRows; x++)
        {
            for (int y = -halfColumns; y < halfColumns; y++)
            {
                Vector3 position = new Vector3(x * cellSize, y * cellSize, 0);
                int cellValue = wfcGrid[x + halfRows, y + halfColumns];
                GameObject cell = CreateCell(x, y, position, cellValue);
                cell.transform.parent = parent.transform;
                gridArray[x + halfRows, y + halfColumns] = cell; // Adjust for zero-based index
            }
        }
    }

    void UpdateGrid() {
        GameObject oldGrid = GameObject.Find("GridHolder");
        Destroy(oldGrid);
        GameObject newGrid = new GameObject("GridHolder");
        newGrid.transform.parent = GameObject.Find("GridCreator").transform;
        CreateGrid(newGrid);
    }

    GameObject CreateCell(int x, int y, Vector3 position, int cellValue) {
        GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity);
        cell.name = $"Cell {x},{y}";
        cell.transform.parent = transform;
        cell.transform.localScale = new Vector3(cellSize, cellSize, 1);

        CellController cellControl = cell.GetComponent<CellController>();
        cellControl.Initialize(x,y,controlledColor,"test");
        cellControl.cellType = cellValue;

        return cell;
    }
}
