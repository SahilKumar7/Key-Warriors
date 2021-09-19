using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    public PianoTile pianoTile; // current number of this grid

    // check if have number
    public bool IsHavePianoTile(){
        return pianoTile != null;
    }


    public PianoTile GetPianoTile(){
        return pianoTile;
    }


    public void SetPianoTile(PianoTile pianoTile){
        this.pianoTile = pianoTile;

    }

    public void SetMyGridColorWhile(){

    }

    public void SetMyGridColorTransparent(){
        
    }
    
}
