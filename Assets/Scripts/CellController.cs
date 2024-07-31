using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{

    public int x;
    public int y;
    public int cellType;

    public Color friendlyColor = Color.blue;
    public Color enemyColor = Color.red;
    private Color cellColor;

    public string controller;

    [SerializeField] SpriteRenderer cellRenderer;

    private CellInfoManager cellInfoManager;

    void Start() {
        // gotta figure out to simplify
        switch (cellType) {
            case 0:
                cellColor = new Color(0f, 100f / 255f, 0f);
                break;
            case 1:
                cellColor = Color.blue;
                break;
            case 2:
                cellColor = Color.grey;
                break;
            case 3:
                cellColor = Color.black;
                break;
        }
        cellRenderer.color = cellColor;

        cellInfoManager = FindFirstObjectByType<CellInfoManager>();
    }

    void OnMouseEnter() {cellRenderer.color = Color.white;}

    void OnMouseExit() {
        cellRenderer.color = cellColor;
    }

    void OnMouseDown() {
        Debug.Log($"Cell {x}, {y} clicked");
        
        if (cellInfoManager != null) cellInfoManager.ShowCellInfo(cellType, x, y, controller);
    }

    public void Initialize(int x, int y, Color controlledColor, int cellType, string controller) {
        this.x = x;
        this.y = y;
        this.cellType = cellType;
        this.controller = controller;
    }
}
