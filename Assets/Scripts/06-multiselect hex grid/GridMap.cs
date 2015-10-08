using UnityEngine;
using System.Collections.Generic;

public class GridMap : MonoBehaviour {
   //Store the Mesh | Drag in Inspector
    public GameObject hex;
    private GameObject[,] hexes;
    
    //How big is this gonna be?
    public int gridWidthInHexes = 10;
    public int gridHeightInHexes = 10;
    public float spacer = 0.02f; //wild hair addition | add a gap between hexes

    //Store info about the supplied Hex
    private GameObject gridParent;
    private float hexWidth;
    private float hexHeight;
    private Vector3 InitialPosition;    //Place first hex at upper-left corner of grid

    void Start() {
        hexWidth =  hex.GetComponent<Renderer>().bounds.size.x + spacer;
        hexHeight = hex.GetComponent<Renderer>().bounds.size.y + spacer;
        
        
        //Center of grid will 0,0,0. Find upper-left position of the first Hex: on 10x10 grid with R=1, something like -4.5, 4.5, 0
        InitialPosition = new Vector3(  -hexWidth * gridWidthInHexes / 2f + hexWidth / 2,   // x
                                        gridHeightInHexes / 2f * hexHeight - hexHeight / 2, // y
                                        -0.0f);                                             // z  | 2d so just hard code. 
                                                                                            //      If 3d swap y & z
        
        if (GameObject.Find("HexagonGrid") != null) {
            gridParent = GameObject.Find("HexagonGrid");
        } else {
            gridParent = new GameObject("HexagonGrid");
            gridParent.tag = "hexgrid";
        };
           
        CreateHexGrid();
    }
    
    
    public List<GridTile> GetAdjacentTiles(Vector2 pPosition) {
        int x = (int)pPosition.x;
        int y = (int)pPosition.y;
        int w = gridWidthInHexes - 1;
        int h = gridHeightInHexes - 1;

       List<GridTile> Adjacents = new List<GridTile>();
       
       if (x > 0) {
            Adjacents.Add(hexes[x-1,y].GetComponent<GridTile>());
        }
        if (x < w) {
            Adjacents.Add(hexes[x+1,y].GetComponent<GridTile>());
        }
        if (y > 0) {  //the two above
            if (y % 2 == 0) { //even row
                Adjacents.Add(hexes[x, y - 1].GetComponent<GridTile>());
                if (x != 0) Adjacents.Add(hexes[x - 1,y - 1].GetComponent<GridTile>());
            } else { //odd rows
                Adjacents.Add(hexes[x,y - 1].GetComponent<GridTile>());
                if (x < w) Adjacents.Add(hexes[x + 1,y - 1].GetComponent<GridTile>());
            }
        }
        if (y < h) {  //the two below
            if (y % 2 == 0) { //even row
                Adjacents.Add(hexes[x, y + 1].GetComponent<GridTile>());
                if (x != 0) Adjacents.Add(hexes[x - 1,y + 1].GetComponent<GridTile>());
            } else { //odd rows
                Adjacents.Add(hexes[x,y + 1].GetComponent<GridTile>());
                if (x < w) Adjacents.Add(hexes[x + 1,y + 1].GetComponent<GridTile>());
            }            
        }          
        return Adjacents;
    }
    
       
    public void Test(Vector2 pPosition) {
        int x = (int)pPosition.x;
        int y = (int)pPosition.y;
        int w = gridWidthInHexes - 1;
        int h = gridHeightInHexes - 1;
        
        
        //if ((x > 0 && y > 0) && (x < w & y < h)) {
        //if (x > 0) {
        //    SpriteRenderer sr = hexes[x-1,y].GetComponent<SpriteRenderer>();      
        //    GridTile gt = hexes[x-1,y].GetComponent<GridTile>();
        //    gt.SetVisual(sr, true);      
        //}
        if (x < w) {
            GridTile gt = hexes[x+1,y].GetComponent<GridTile>();
            gt.IsSelected = true;
            gt.SetVisual();
        }        
        
        
        
        return;
        if (hexes[x-1,y]) {
            Debug.Log("HERE???");
            if (hexes[x-1,y].GetComponent<GridTile>()) { 
                
                //if (x >= 0 && y >= 0 && x < w && y < h) {
                    
                
                
                SpriteRenderer sr = hexes[x-1,y].GetComponent<SpriteRenderer>();
                GridTile gt = hexes[x-1,y].GetComponent<GridTile>();
                gt.Test();
                gt.SetVisual();
                
                //Debug.Log("gridtiles");  
            }
        }
                
        Debug.Log("from map test: " + pPosition.ToString());
    }
    

    void CreateHexGrid() {
        hexes = new GameObject[gridWidthInHexes, gridHeightInHexes];
        
        
        //Loop for Hex Rows
        for (int y = 0; y < gridHeightInHexes; y++)
        {
            //Loop for Hex Columns
            for (int x = 0; x < gridWidthInHexes; x++)
            {
                Vector2 gridPosition = new Vector2(x, y);
                
                //Create a clone of the supplied Hex object
                //GameObject hex = (GameObject)Instantiate(Hex);
                hexes[x, y] = (GameObject)Instantiate(hex);
                
                hexes[x, y].GetComponent<GridTile>().myGridPosition = gridPosition;
               
                
                //Get the current x,y of loop to place Hex | column 5, row 3, etc.
                //Vector2 gridPosition = new Vector2(x, y);
                
                //Center of grid is 0,0,0 figure out the pixel coordinates of this hex based on it's x,y
                //hex.transform.position = calculateWorldCoordinates(gridPosition);
                hexes[x, y].transform.position = CalculateWorldCoordinates(gridPosition);
                
                //Add the hex to the parent ojbect
                //hex.transform.parent = gridParent.transform;
                hexes[x, y].transform.parent = gridParent.transform;
                
            }
        }
    }
    
    //Translate an x,y grid position to screen pixel coordinates
    public Vector3 CalculateWorldCoordinates(Vector2 pHexPosition) {
        //Hexagons, how do they work? MATHS! 
        //--New rows are moved down 3/4ths of the height (in 2d this is y | in 3d this is z)
        //--and moved left or right by half the width depending on the row.
        
        //Hex with point-y side up : Every other row is offset by half of the width
        float offset = 0;               //first row normal, alternating rows get the 1/2 bump
        if (pHexPosition.y % 2 != 0)    //even rows = 0
            offset = hexWidth / 2;      //odd rows = width / 2
        
        //Parenthesis for readablity, not technically needed.
        //Find the x coordinate based on x & width things
        float x = InitialPosition.x + offset + (pHexPosition.x * hexWidth);
        
        //This is a 2d generator, so offset y for each new row by 3/4ths the hight
        float y = InitialPosition.y - (pHexPosition.y * hexHeight * 0.75f);
        return new Vector3(x, y, 0.2f); //Since 2d just hardcoding a z to whatever works (would be y in 3d)
    }      
}
