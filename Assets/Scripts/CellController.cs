using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{

    public int x;
    public int y;

    public Color controlledColor;
    public Color hoverColor;

    public string currController;
    public string cellType;

    //[SerializeField] SpriteRenderer cellRenderer;

    void OnMouseEnter() {
        //cellRenderer.material.color = hoverColor;
    }

    void OnMouseExit() {
        //.material.color = controlledColor;
    }

    void OnMouseDown() {
        Debug.Log($"Cell {x}, {y} clicked");
        // do more
    }

    public void Initialize(int x, int y, Color controlledColor, Color hoverColor, string currController) {
        this.x = x;
        this.y = y;
        this.controlledColor = controlledColor;
        this.hoverColor = hoverColor;
    }
}
