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
    [SerializeField] TMP_Text coordinatesText;

    // Start is called before the first frame update
    void Start()
    {
        cellInfoPanel.SetActive(false);
    }

    public void ShowCellInfo(int cellType, int x, int y) {
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
        }
        coordinatesText.text = "Cell: (" + x + ", " + y +")";
    }

    public void HideCellInfo() {cellInfoPanel.SetActive(false);}
}
