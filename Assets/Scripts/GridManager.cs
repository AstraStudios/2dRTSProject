using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using Unity.Collections;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField] int rows;
    [SerializeField] int columns;
    [SerializeField] float cellSize;

    Color hoverColor = Color.gray;
    Color controlledColor = Color.red;

    [SerializeField] GameObject cellPrefab;
    GameObject[,] gridArray;

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }

    void CreateGrid() {
        gridArray = new GameObject[rows, columns];

        int halfRows = rows / 2;
        int halfColumns = columns / 2;

        for (int x = -halfRows; x < halfRows; x++)
        {
            for (int y = -halfColumns; y < halfColumns; y++)
            {
                Vector3 position = new Vector3(x * cellSize, y * cellSize, 0);
                GameObject cell = CreateCell(x, y, position);
                gridArray[x + halfRows, y + halfColumns] = cell; // Adjust for zero-based index
            }
        }
    }

    GameObject CreateCell(int x, int y, Vector3 position) {
        GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity);
        cell.name = $"Cell {x},{y}";
        cell.transform.parent = transform;
        cell.transform.localScale = new Vector3(cellSize, cellSize, 1);

        CellController cellControl = cell.GetComponent<CellController>();
        cellControl.Initialize(x,y,controlledColor,hoverColor,"test");

        return cell;
    }
}
