using UnityEngine;


public class HexagonMesh : MonoBehaviour {

    void Awake() {
        GameObject hexagon = new GameObject("HexagonMeshTile");
        MeshFilter meshFilter = (MeshFilter)hexagon.AddComponent(typeof(MeshFilter));
        meshFilter.mesh = CreateHexagon(0.4f);
        MeshRenderer renderer = hexagon.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        
        hexagon.AddComponent<MeshCollider>();
        hexagon.AddComponent<GridMouse>();
        //renderer.material.shader = Shader.Find("Particles/Additive");
        Texture2D tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.red);

        tex.Apply();
        renderer.material.mainTexture = tex;
        renderer.material.color = Color.green;
    }

    Mesh CreateHexagon(float radius) {
        const float cos60 = 0.5f;
        const float sin60 = 0.86603f;

        Mesh mesh = new Mesh();
        mesh.name = "dynaHex";
        

        Vector3[] vertices = new Vector3[6];
        Vector2[] uvs = new Vector2[6];
        int[] triangles = new int[12] { 1,5,0, 2,3,4,
                                                2,4,1, 1,4,5};
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
        vertices[0] = -Vector3.right * radius;
        uvs[0] = new Vector2(0, 0.5f);

        vertices[1] = -Vector3.right * radius * cos60 + Vector3.up * radius * sin60;
        uvs[1] = new Vector2(0.25f, 1);

        vertices[2] = Vector3.right * radius * cos60 + Vector3.up * radius * sin60;
        uvs[2] = new Vector2(0.75f, 1);

        vertices[3] = Vector3.right * radius;
        uvs[3] = new Vector2(1, 0);

        vertices[4] = Vector3.right * radius * cos60 - Vector3.up * radius * sin60;
        uvs[4] = new Vector2(0.75f, 0);

        vertices[5] = -Vector3.right * radius * cos60 - Vector3.up * radius * sin60;
        uvs[5] = new Vector2(0.25f, 0);

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();

        return mesh;

    }
}
