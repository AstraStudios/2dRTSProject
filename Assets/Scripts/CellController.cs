using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{

    public int x;
    public int y;

    public Color friendlyColor = Color.blue;
    public Color enemyColor = Color.red;

    public string currController;
    public int cellType;

    [SerializeField] SpriteRenderer cellRenderer;

    private CellInfoManager cellInfoManager;

    void Start() {
        switch (cellType) {
            case 0:
                cellRenderer.color = new Color(0f, 100f / 255f, 0f);
                break;
            case 1:
                cellRenderer.color = Color.blue;
                break;
            case 2:
                cellRenderer.color = Color.grey;
                break;
        }

        cellInfoManager = FindObjectOfType<CellInfoManager>();
    }

    void OnMouseEnter() {
        cellRenderer.color = Color.white;
    }

    void OnMouseExit() {
        switch (cellType) {
            case 0:
                cellRenderer.color = new Color(0f, 100f / 255f, 0f);
                break;
            case 1:
                cellRenderer.color = Color.blue;
                break;
            case 2:
                cellRenderer.color = Color.grey;
                break;
        }
    }

    void OnMouseDown() {
        Debug.Log($"Cell {x}, {y} clicked");
        
        if (cellInfoManager != null) cellInfoManager.ShowCellInfo(cellType, x, y);
    }

    public void Initialize(int x, int y, Color controlledColor, string currController) {
        this.x = x;
        this.y = y;
        this.friendlyColor = controlledColor;
    }
}
