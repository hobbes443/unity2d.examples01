using UnityEngine;

public class MeshAndGrid : MonoBehaviour {

	GameObject hexagon;

    //How big is this gonna be?
    public int gridWidthInHexes = 10;
    public int gridHeightInHexes = 10;
    public float hexRadius = 0.4f;
    public float spacer = 0.04f; //wild hair addition | add a gap between hexes

    //Store info about the supplied Hex
    private float hexWidth;
    private float hexHeight;
    private Vector3 InitialPosition;    //Place first hex at upper-left corner of grid

    void Awake() {
        hexagon = new GameObject("HexagonMeshTile");
        MeshFilter meshFilter = (MeshFilter)hexagon.AddComponent(typeof(MeshFilter));
        meshFilter.mesh = CreateHexagon(hexRadius);
        MeshRenderer renderer = hexagon.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        
        hexagon.AddComponent<MeshCollider>();
        hexagon.AddComponent<GridMouse>();
        //renderer.material.shader = Shader.Find("Particles/Additive");
        Texture2D tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.red);

        tex.Apply();
        renderer.material.mainTexture = tex;
        renderer.material.color = Color.green;
		
        //hexagon.SetActive(false);
		//return hexagon;
    }
	
    void Start() {
        hexWidth =  hexagon.GetComponent<Renderer>().bounds.size.x + spacer;
        hexHeight = hexagon.GetComponent<Renderer>().bounds.size.y + spacer;
        
        //Center of grid will 0,0,0. Find upper-left position of the first Hex
        InitialPosition = new Vector3(  -hexWidth * gridWidthInHexes / 2f + hexWidth / 2,   // x
                                        gridHeightInHexes / 2f * hexHeight - hexHeight / 2, // y
                                        -0.0f);                                             // z  | 2d so just hard code. 
                                                                                            //      If 3d swap y & z
        if (GameObject.Find("HexagonGrid") != null) {
            DestroyImmediate(GameObject.Find("HexagonGrid"));
        };
           
        CreateHexGrid();
        Destroy(hexagon);
    }

    void CreateHexGrid() {
        //Create a parent object for all the hexes
        GameObject hexagonGridParent = new GameObject("HexagonGrid");
        hexagonGridParent.tag = "hexgrid";

        //Loop for Hex Rows
        for (float y = 0; y < gridHeightInHexes; y++)
        {
            //Loop for Hex Columns
            for (float x = 0; x < gridWidthInHexes; x++)
            {
                //Create a clone of the supplied Hex object
                GameObject hex = (GameObject)Instantiate(hexagon);
                
                //Get the current x,y of loop to place Hex | column 5, row 3, etc.
                Vector2 gridPosition = new Vector2(x, y);
                
                //Center of grid is 0,0,0 figure out the pixel coordinates of this hex based on it's x,y
                hex.transform.position = calculateWorldCoordinates(gridPosition);
                
                //Add the hex to the parent ojbect
                hex.transform.parent = hexagonGridParent.transform;
                //hex.transform.parent = transform;
            }
        }
    }
    
    //Translate an x,y grid position to screen pixel coordinates
    public Vector3 calculateWorldCoordinates(Vector2 pHexPosition) {
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
    
    
    Mesh CreateHexagon(float radius) {
        const float cos60 = 0.5f;
        const float sin60 = 0.86603f;

        Mesh mesh = new Mesh();
        mesh.name = "dynaHex";
        

        Vector3[] vertices = new Vector3[6];
        Vector2[] uvs = new Vector2[6];
        int[] triangles = new int[12] { 1,0,5, 2,4,3,
                                                2,1,4, 1,5,4};
        // Vertex layout
        //   1---2
        //  / \ / \
        // 0---x---3
        //  \ / \ /
        //   5---4
        // =============
        // r = 1 (to vertices)
        // triangles are equilateral,
        // so each inner angle == 60 deg
        vertices[0] = -Vector3.up * radius;
        uvs[0] = new Vector2(0, 0.5f);

        vertices[1] = -Vector3.up * radius * cos60 + Vector3.right * radius * sin60;
        uvs[1] = new Vector2(0.25f, 1);

        vertices[2] = Vector3.up * radius * cos60 + Vector3.right * radius * sin60;
        uvs[2] = new Vector2(0.75f, 1);

        vertices[3] = Vector3.up * radius;
        uvs[3] = new Vector2(1, 0);

        vertices[4] = Vector3.up * radius * cos60 - Vector3.right * radius * sin60;
        uvs[4] = new Vector2(0.75f, 0);

        vertices[5] = -Vector3.up * radius * cos60 - Vector3.right * radius * sin60;
        uvs[5] = new Vector2(0.25f, 0);

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();

        return mesh;

    }
}
