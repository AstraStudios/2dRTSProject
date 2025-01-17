using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFunctionCollapse
{
    private int[,] grid;
    private int rows;
    private int columns;
    private List<int> possibleValues;
    private Dictionary<Vector2Int, string> cellControllers;
    int value = 0;

    public WaveFunctionCollapse(int rows, int columns, List<int> possibleValues)
    {
        this.rows = rows;
        this.columns = columns;
        this.possibleValues = possibleValues;
        grid = new int[rows, columns];
        cellControllers = new Dictionary<Vector2Int, string>();
    }

    public int[,] GenerateGrid(out List<Vector2Int> friendlyArea, out List<Vector2Int> enemyArea)
    {
        friendlyArea = new List<Vector2Int>();
        enemyArea = new List<Vector2Int>();

        // Initialize the grid with -1 indicating unassigned cells
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                grid[x, y] = -1;
            }
        }

        // Define friendly and enemy starting areas
        for (int i = 0; i < rows/2; i++) for (int o = 0; o < columns/2; o++) DefineStartingArea(friendlyArea, i, o, "Friendly"); 
        for (int t = rows/2; t < rows; t++) for (int k = columns/2; k < columns; k++) DefineStartingArea(enemyArea, t, k, "Enemy");

        // Fill in the rest of the grid with the WFC algorithm
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                if (grid[x, y] == -1) // Only fill cells that are not part of starting areas
                {
                    int cellValue = DetermineCellValue();
                    grid[x, y] = cellValue;
                }
            }
        }

        return grid;
    }

    private void DefineStartingArea(List<Vector2Int> area, int startX, int startY, string controller) {
        for (int x = startX; x < startX + 2; x++) {
            for (int y = startY; y < startY + 3; y++){
                Vector2Int position = new Vector2Int(x,y);
                area.Add(position);
                cellControllers[new Vector2Int(x,y)] = controller;
            }
        }
    }

    public string GetCellController(int x, int y) {
        Vector2Int pos = new Vector2Int(x,y);
        if (cellControllers.ContainsKey(pos)) return cellControllers[pos];
        return string.Empty;
    }

    private int DetermineCellValue()
    {
        int rand = Random.Range(0, 100);
        switch (value)
        {
            case 0: // grass
                if (rand < 65) return 0;
                if (rand > 65) value = Random.Range(0, possibleValues.Count);
                break;
            case 1: // water
                if (rand < 40) return 1;
                if (rand > 40) value = Random.Range(0, possibleValues.Count);
                break;
            case 2: // mountains
                if (rand < 10) return 2;
                if (rand > 10) value = Random.Range(0, possibleValues.Count);
                break;
            case 3: // salvage mines
                if (rand < 5) return 3;
                if (rand > 5) value = Random.Range(0, possibleValues.Count);
                break;
        }

        return possibleValues[0];
    }
}
