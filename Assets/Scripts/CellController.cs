using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{

    public int x;
    public int y;
    public int cellType;
    private int colorIndex = 1;
    public float metalsAmount;
    public float fuelAmount;

    Color controlledColor;
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

    void Update() {
        if (controller == "Friendly") controlledColor = Color.blue;
        if (controller == "Enemy") controlledColor = Color.red;
        if (controller == null) controlledColor = Color.gray;
        CurrColorMode(cellColor, controlledColor);
    }

    void OnMouseEnter() {cellRenderer.color = Color.white;}

    void OnMouseExit() {
        cellRenderer.color = cellColor;
    }

    void OnMouseDown() {
        Debug.Log($"Cell {x}, {y} clicked");
        
        if (cellInfoManager != null) cellInfoManager.ShowCellInfo(cellType, x, y, metalsAmount, fuelAmount, controller);
        //ChangeOwnerToFriendly();
    }

    private void CurrColorMode(Color cellColor, Color currControllerColor) {
        if (colorIndex == 1) cellRenderer.color = cellColor;
        if (colorIndex == 2) cellRenderer.color = currControllerColor;
        if (Input.GetKeyDown(KeyCode.O)) {
            switch (colorIndex) {
                case 1:
                    colorIndex++;
                    break;
                case 2:
                    colorIndex--;
                    break;
            }
        }
    }

    public void Initialize(int x, int y, int cellType, float metals, float fuel, string controller) {
        this.x = x;
        this.y = y;
        this.cellType = cellType;
        this.metalsAmount = metals;
        this.fuelAmount = fuel;
        this.controller = controller;
    }

    //public void ChangeOwnerToFriendly() {this.controller = "Friendly";}
}
