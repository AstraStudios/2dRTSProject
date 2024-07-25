using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class WaveFunctionCollapse
{
    private int[,] grid;
    private int rows;
    private int columns;
    private List<int> possibleValues;
    int value = 0;

    public WaveFunctionCollapse(int rows, int columns, List<int> possibleValues)
    {
        this.rows = rows;
        this.columns = columns;
        this.possibleValues = possibleValues;
        grid = new int[rows, columns];
    }

    public int[,] GenerateGrid()
    {
        // Initialize the grid with -1 indicating unassigned cells
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                grid[x, y] = -1;
            }
        }

        // Apply the WFC algorithm to fill the grid
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                int cellValue = DetermineCellValue();
                grid[x,y] = cellValue;
            }
        }

        return grid;
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
        }

        return possibleValues[0];
    }
}
