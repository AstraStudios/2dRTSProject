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
        //                                       Grass, Water, Mountain, Salvage Depot
        List<int> possibleValues = new List<int> {0,1,2,3};

        WaveFunctionCollapse wfc = new WaveFunctionCollapse(rows, columns, possibleValues);
        int[,] wfcGrid = wfc.GenerateGrid(out List<Vector2Int> friendlyArea, out List<Vector2Int> enemyArea);

        int halfRows = rows / 2;
        int halfColumns = columns / 2;

        for (int x = -halfRows; x < halfRows; x++)
        {
            for (int y = -halfColumns; y < halfColumns; y++)
            {
                Vector3 position = new Vector3(x * cellSize, y * cellSize, 0);
                int cellValue = wfcGrid[x + halfRows, y + halfColumns];
                string controller = wfc.GetCellController(x+halfRows, y+halfColumns);

                Vector2Int cellPos = new Vector2Int(x + halfRows, y + halfColumns);
                if (friendlyArea.Contains(cellPos)) controller = "Friendly";
                if (enemyArea.Contains(cellPos)) controller = "Enemy";

                GameObject cell = CreateCell(x, y, position, cellValue, controller);
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

    GameObject CreateCell(int x, int y, Vector3 position, int cellValue, string controller) {
        GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity);
        cell.name = $"Cell {x},{y}";
        cell.transform.parent = transform;
        cell.transform.localScale = new Vector3(cellSize, cellSize, 1);

        CellController cellControl = cell.GetComponent<CellController>();
        if (cellValue == 0) cellControl.Initialize(x, y, cellValue, Mathf.RoundToInt(Random.Range(0f,100f)), Mathf.RoundToInt(Random.Range(0f,250f)), controller);
        if (cellValue == 1) cellControl.Initialize(x, y, cellValue, 0, Mathf.RoundToInt(Random.Range(30f, 500f)), controller);
        if (cellValue == 2) cellControl.Initialize(x, y, cellValue, Mathf.RoundToInt(Random.Range(0f, 250f)), Mathf.RoundToInt(Random.Range(0f,15f)), controller);
        if (cellValue == 3) cellControl.Initialize(x, y, cellValue, Mathf.RoundToInt(Random.Range(100f, 500f)), Mathf.RoundToInt(Random.Range(100f, 300f)), controller);
        cellControl.cellType = cellValue;

        return cell;
    }
}
