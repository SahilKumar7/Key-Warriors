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
    public LoseMenuBtn loseMenuBtn;
    private int row; // number of rows

    private int col; // number of columns

    public GameObject tilePrefab;
    public GameObject pianoTilePrefab;
    public float timer = 1;

    public List<MyGrid> canCreatePianoTileGrid = new List<MyGrid>();

    public MyGrid[][] grids = null; // save all the generated grids

    public int tilesPlayed;
    public float speed;

    private bool gameFinished = false;

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

    public void InitCanCreateTileGrid(){
        for (int j = 0; j < col; j++){
            canCreatePianoTileGrid.Add(grids[0][j]);
        }
    }

public void CreatePianoTile(){
               //Randomly choose one of the elements  
        int tilesPerRow = RandomizerProbability();      //The int at the element is tile per row.

        if(tilesPerRow > 0){                                  //If tilesPerRow is more than 0 than we make a tile
            var tilesIndexes = new List<int>();               //Create an array list to store positions with tiles
            int index = Random.Range(0, col);                 //Since there is definetly a tile, than we get an index for it
            tilesIndexes.Add(index);                          //And we store it. 

            int i = 1;                                        // init while loop
            while(i < tilesPerRow){                           //if there is one tile per row, we already made it, no need to need to make more. 
                int newIndex = Random.Range(0, col);          //we get a random index for a new tile
                if(!tilesIndexes.Contains(newIndex)){         // check to make sure it doesnt overlap
                    tilesIndexes.Add(newIndex);               //store the new index
                    i++;                                      //iterate next loop
                }  
            }
            for(int j = 0; j < tilesIndexes.Count; j++)       //Now we print the tiles at the indexes.
            {          
                PlacePianoTile(tilesIndexes[j]);
            }
        }
    } 

public int RandomizerProbability(){
    /*Probability Ratios {zeroTile, oneTile, twoTiles, threeTiles}
    int[] easy = {2,1,7,0};
    int[] medium = {1,5,3,1};
    int[] hard = {0,3,1,6}; */

    if(tilesPlayed < 30){                  //After 120 tilesPlayed
        speed = 1;
        Debug.Log("easy");
        return probabilityGenrtr(0);     //Release the tiles per row. 
    }
    else if(tilesPlayed < 60){
        Debug.Log("medium");
        speed = 0.5f;
        return probabilityGenrtr(1);
    }
    else{
        Debug.Log("hard");
        speed = 0.25f;
        return probabilityGenrtr(2);
    }
}

public int probabilityGenrtr(int level){
    int[] easy = {0,0,0,1,1,1,1,1,1,1};
    int[] medium = {0,0,1,1,1,1,1,2,2,0};
    int[] hard = {0,0,0,1,1,1,1,2,2,3};
    if(level == 0){
        int tilesPerRowIndex = Random.Range(0, 10);  
        return easy[tilesPerRowIndex];
    }
    else if(level == 1){
        int tilesPerRowIndex = Random.Range(0, 10);  
        return medium[tilesPerRowIndex];
    }
    else{
        int tilesPerRowIndex = Random.Range(0, 10);  
        return hard[tilesPerRowIndex];
    }

      //Return the tiles per row.
}

/*
public int probabilityGenrtr(int[] tiles){
    int arrLen = 0;
    for(int i = 0; i < tiles.Length; i++){ //Gather the length required for the probability array.
        arrLen += tiles[i];
    }
    int[] probabilityArr = new int[arrLen]; 
    List<int> probabilityList = new List<int>(); //Make a list to store the ratios.

    for(int i = 0; i < tiles.Length; i++){ //Store the ratios.
        for(int j = 0; j < tiles[i]; j++){
            probabilityList.Add(i);
        }
    }

    for(int i = 0; i < arrLen; i++){
        probabilityArr[i] = probabilityList[i]; //Apply list into array.
    }

    int tilesPerRowIndex = Random.Range(0, 10);   //Gets a random index to choose in the probability array.
    return probabilityArr[tilesPerRowIndex];  //Return the tiles per row.
}
*/
public void PlacePianoTile(int index){
    GameObject gameObject = GameObject.Instantiate(pianoTilePrefab,canCreatePianoTileGrid[index].transform);
    gameObject.GetComponent<PianoTile>().Init(canCreatePianoTileGrid[index], (PlayerKeyType)index);
}

    private void Awake() {
        // initate grid
        InitGrid();
        InitCanCreateTileGrid();
        CreatePianoTile();
        tilesPlayed = 0;
        speed = 1;
        

    }
    public void automate(){
        
    }
    public void MoveDown(){
        tilesPlayed++;
        for(int i = row-1; i >=0; i --){
            for(int j = col-1; j >= 0; j--){
                if (this.grids[i][j].IsHavePianoTile()){
                    // check if PianoTile is at the bottom
                    PianoTile pianoTile = this.grids[i][j].GetPianoTile();
                    if (i == row-1){
                        // you lose
                        Debug.Log("You Lose!");
                        this.gameFinished = true;
                    }
                    else{
                        pianoTile.MoveToGrid(grids[i+1][j]);
                    }
                }
            }
        }
        CreatePianoTile();
    }

    public void OnClickTile(int i){
        if (this.grids[Const.RowNum-1][i].IsHavePianoTile()){
            PianoTile pianoTile = this.grids[Const.RowNum-1][i].GetPianoTile();
        
            pianoTile.SetGrid(null);
            GameObject.Destroy(pianoTile.gameObject);
            Debug.Log("destoryed Tiletype : " + (PlayerKeyType) i);
        }
    }

    void Update()
    {
        if ( (!gameFinished) && (timer > 1)) // this is in seconds (1/speed)
        {
         //Do Stuff
            timer = 0;
            MoveDown();
        }
        timer += UnityEngine.Time.deltaTime;

        for(int i = 0; (i < Const.ColumnNum) && (!gameFinished); i ++){
            if (Input.GetKeyUp(Const.KeyPressList[i]))
            {
                OnClickTile(i);
            }
        }
    }

}

// Version 2
/* 
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
        this.grids = new MyGrid[row][]; .

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

    public void InitCanCreateTileGrid(){
        for (int j = 0; j < col; j++){
            canCreatePianoTileGrid.Add(grids[0][j]);
        }
    }

public void CreatePianoTile(){
        int[] probability = {0, 2, 2, 2, 2, 1, 1, 1, 1, 3};   //Probability of tiles per row. 
        int tilesPerRowIndex = Random.Range(0, 10);           //Randomly choose one of the elements  
        int tilesPerRow = probability[tilesPerRowIndex];      //The int at the element is tile per row.

        if(tilesPerRow > 0){                                  //If tilesPerRow is more than 0 than we make a tile
            var tilesIndexes = new List<int>();               //Create an array list to store positions with tiles
            int index = Random.Range(0, col);                 //Since there is definetly a tile, than we get an index for it
            tilesIndexes.Add(index);                          //And we store it. 

            int i = 1;                                        // init while loop
            while(i < tilesPerRow){                           //if there is one tile per row, we already made it, no need to need to make more. 
                int newIndex = Random.Range(0, col);          //we get a random index for a new tile
                if(!tilesIndexes.Contains(newIndex)){         // check to make sure it doesnt overlap
                    tilesIndexes.Add(newIndex);               //store the new index
                    i++;                                      //iterate next loop
                }  
            }
            for(int j = 0; j < tilesIndexes.Count; j++)       //Now we print the tiles at the indexes.
            {          
                PlacePianoTile(tilesIndexes[j]);
            }
        }
    } 

public void PlacePianoTile(int index){
    GameObject gameObject = GameObject.Instantiate(pianoTilePrefab,canCreatePianoTileGrid[index].transform);
    gameObject.GetComponent<PianoTile>().Init(canCreatePianoTileGrid[index]);
}

    private void Awake() {
        // initate grid
        InitGrid();
        InitCanCreateTileGrid();
        CreatePianoTile();
    }

    public void MoveDown(){
        for(int i = row-1; i >=0; i --){
            for(int j = col-1; j >= 0; j--){
                if (grids[i][j].IsHavePianoTile()){
                    // check if PianoTile is at the bottom
                    PianoTile pianoTile = grids[i][j].GetPianoTile();
                    if (i == row-1){
                        // destory tile
                        pianoTile.SetGrid(null);
                        GameObject.Destroy(pianoTile.gameObject);
                        // you lose
                    }
                    else{
                        pianoTile.MoveToGrid(grids[i+1][j]);
                    }
                }
            }
        }
        CreatePianoTile();
    }
} */

//Version1: 

/*
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

    public void InitCanCreateTileGrid(){
        for (int j = 0; j < col; j++){
            canCreatePianoTileGrid.Add(grids[0][j]);
        }
    }
    public void CreatePianoTile(){

        int index = Random.Range(0, col);
        GameObject gameObject = GameObject.Instantiate(pianoTilePrefab,canCreatePianoTileGrid[index].transform);
        gameObject.GetComponent<PianoTile>().Init(canCreatePianoTileGrid[index]);
    }

/*
public void CreatePianoTile(){
        int tilesPerRow = Random.Range(0, 3);                 //Randomize the amount of tiles per row
    
        if(tilesPerRow > 0){                                  //If tilesPerRow is more than 0 than we make a tile
            List<Integer> tilesIndexes = new List<Interger>();//Create an array list to store positions with tiles
            int index = Random.Range(0, col);                 //Since there is definetly a tile, than we get an index for it
            tilesIndexes.add(index);                          //And we store it. 

            int i = 1;                                        // init while loop
            while(i < tilesPerRow){                           //if there is one tile per row, we already made it, no need to need to make more. 
                int newIndex = Random.Range(0, col);          //we get a random index for a new tile
                if(!tilesIndexes.Contains(newIndex)){         // check to make sure it doesnt overlap
                    tilesIndexes.add(newIndex);               //add the new index
                    i++;                                      //iterate next loop
                }  
            }
        }
        
        for(int i = 0; i < tilesIndexes.Count; i++){          //Now we print the tiles at the indexes.
            PlacePianoTile(tilesIndexes.ElementAt(i));
        }
    } 

public void PlacePianoTile(int index){
    GameObject gameObject = GameObject.Instantiate(pianoTilePrefab,canCreatePianoTileGrid[index].transform);
    gameObject.GetComponent<PianoTile>().Init(canCreatePianoTileGrid[index]);
}


    private void Awake() {
        // initate grid
        InitGrid();
        InitCanCreateTileGrid();
        CreatePianoTile();
    }

    public void MoveDown(){
        for(int i = row-1; i >=0; i --){
            for(int j = col-1; j >= 0; j--){
                if (grids[i][j].IsHavePianoTile()){
                    // check if PianoTile is at the bottom
                    PianoTile pianoTile = grids[i][j].GetPianoTile();
                    if (i == row-1){
                        // destory tile
                        pianoTile.SetGrid(null);
                        GameObject.Destroy(pianoTile.gameObject);
                        // you lose
                    }
                    else{
                        pianoTile.MoveToGrid(grids[i+1][j]);
                    }
                }
            }
        }
        CreatePianoTile();
    }
}
*/