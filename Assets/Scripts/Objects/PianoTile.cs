using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PianoTile : MonoBehaviour
{ 
    private Image bg;

    private MyGrid parentGrid;

    private void Awake() {
        bg = transform.GetComponent<Image>();
    }

    // initialize
    public void Init(MyGrid myGrid){
        myGrid.SetPianoTile(this);
        this.SetGrid(myGrid);
    }

    public void SetGrid(MyGrid myGrid){
        parentGrid = myGrid;
    }

    public MyGrid GetGrid(){
        return parentGrid;
    }

    public void MoveToGrid(MyGrid myGrid){
        transform.SetParent(myGrid.transform);
        transform.localPosition = Vector3.zero;

        GetGrid().SetPianoTile(null);
        myGrid.SetPianoTile(this);
        SetGrid(myGrid);
    }
}
