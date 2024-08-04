using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class CellInfoManager : MonoBehaviour
{
    
    public GameObject cellInfoPanel;
    [SerializeField] TMP_Text cellTypeText;
    [SerializeField] TMP_Text cellNameText;
    [SerializeField] TMP_Text metalAmountText;
    [SerializeField] TMP_Text fuelAmountText;
    [SerializeField] TMP_Text controllerText;

    // Start is called before the first frame update
    void Start()
    {
        cellInfoPanel.SetActive(false);
    }

    public void ShowCellInfo(int cellType, int x, int y, float metalsAmount, float fuelAmount, string controller) {
        cellInfoPanel.SetActive(true);
        switch (cellType) {
            case 0:
                cellTypeText.text = "Cell Type: Ground";
                break;
            case 1:
                cellTypeText.text = "Cell Type: Water";
                break;
            case 2:
                cellTypeText.text = "Cell Type: Mountain";
                break;
            case 3:
                cellTypeText.text = "Cell Type: Salvage Spot";
                break;
        }
        cellNameText.text = "Cell: (" + x + ", " + y +")";
        metalAmountText.text = metalsAmount + " metals";
        fuelAmountText.text = fuelAmount + " fuel";
        controllerText.text = "Controlled by: " + controller;
    }

    public void HideCellInfo() {cellInfoPanel.SetActive(false);}
}
