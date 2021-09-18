using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    public Text text_score; // current score
    public Text text_best_score; // best score

    public Button btn_restart;

    public Button btn_exit;

    public Transform girdParent; // parent of grid

    private int row; // number of rows

    private int col; // number of columns

    public GameObject tilePrefab;
    public GameObject pianoTilePrefab;


    public List<MyGrid> canCreatePianoTileGrid = new List<MyGrid>();
    


    public MyGrid[][] grids = null; // save all the generated grids

    // restart
    public void OnRestartClick(){

    }

    // exit
    public void OnExitClick(){

    }

    // initialize number of grids
    public void InitGrid(){

        // create number of grids
        // 4*6 cell size 45 * 45
        GridLayoutGroup gridLayoutGroup = girdParent.GetComponent<GridLayoutGroup>();
        gridLayoutGroup.constraintCount = 6;
        gridLayoutGroup.cellSize = new Vector2(45,45);

        // this is the current setting
        row = Const.RowNum;
        col = Const.ColumnNum;
        this.grids = new MyGrid[row][];

        for (int i = 0; i < row; i++){
            for(int j = 0; j < col; j++){
                if(this.grids[i] == null){
                    this.grids[i] = new MyGrid[col];
                }
                // create i, j cell
                this.grids[i][j] = CreateGrid();
            }
        }
    }
    public MyGrid CreateGrid(){
        //create instances from profabs
        GameObject gameObject = GameObject.Instantiate(tilePrefab,girdParent);
        return gameObject.GetComponent<MyGrid>();
    }

    public void CreatePianoTile(){
        // canCreateNumberGrid.Clear();

    }

    private void Awake() {
        // initate grid
        InitGrid();

    }
}
