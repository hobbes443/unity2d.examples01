using UnityEngine;
using System.Collections;

public class HexMesh : MonoBehaviour {

    public Mesh testMesh;

    void Awake() {
        testMesh = Generate(3.0f);
    }

    void Start() {
        GameObject hexMeshGO = new GameObject("HexagonMesh");

        hexMeshGO.AddComponent<MeshFilter>().mesh = testMesh;
        hexMeshGO.AddComponent<MeshRenderer>();
          

        //GameObject hex = (GameObject)Instantiate(testMesh);
    }

    public Mesh Generate(float height) {
        float diff = height / 2.0f;
        Mesh mesh = new Mesh();

        // top chunk
        Vector3 v0 = new Vector3(0.0f, diff, 0.0f);
        // back
        Vector3 v1 = new Vector3(-0.5f, diff, -1.25f);
        Vector3 v2 = new Vector3(0.5f, diff, -1.25f);
        // right
        Vector3 v3 = new Vector3(1.25f, diff, -0.5f);
        Vector3 v4 = new Vector3(1.25f, diff, 0.5f);
        // front
        Vector3 v5 = new Vector3(0.5f, diff, 1.25f);
        Vector3 v6 = new Vector3(-0.5f, diff, 1.25f);
        // left
        Vector3 v7 = new Vector3(-1.25f, diff, 0.5f);
        Vector3 v8 = new Vector3(-1.25f, diff, -0.5f);
        // bottom chunk
        Vector3 v9 = new Vector3(0.0f, -diff, 0.0f);
        // back
        Vector3 v10 = new Vector3(-0.5f, -diff, -1.25f);
        Vector3 v11 = new Vector3(0.5f, -diff, -1.25f);
        // right
        Vector3 v12 = new Vector3(1.25f, -diff, -0.5f);
        Vector3 v13 = new Vector3(1.25f, -diff, 0.5f);
        // front
        Vector3 v14 = new Vector3(0.5f, -diff, 1.25f);
        Vector3 v15 = new Vector3(-0.5f, -diff, 1.25f);
        // left
        Vector3 v16 = new Vector3(-1.25f, -diff, 0.5f);
        Vector3 v17 = new Vector3(-1.25f, -diff, -0.5f);

        mesh.vertices = new Vector3[] {
      v0,
      v1, v2,
      v3, v4,
      v5, v6,
      v7, v8,
      v9,
      v10, v11,
      v12, v13,
      v14, v15,
      v16, v17
    };
        mesh.triangles = new int[] {
      // top
      8, 7, 0,
      7, 6, 0,
      6, 5, 0,
      5, 4, 0,
      4, 3, 0,
      3, 2, 0,
      2, 1, 0,
      1, 8, 0,
      // sides
      10, 1, 2,
      11, 10, 2,
      11, 2, 3,
      12, 11, 3,
      12, 3, 4,
      13, 12, 4,
      13, 4, 5,
      14, 13, 5,
      14, 5, 6,
      15, 14, 6,
      15, 6, 7,
      16, 15, 7,
      16, 7, 8,
      17, 16, 8,
      17, 8, 1,
      10, 17, 1,
      // bottom
      10, 11, 9,
      11, 12, 9,
      12, 13, 9,
      13, 14, 9,
      14, 15, 9,
      15, 16, 9,
      16, 17, 9,
      17, 10, 9
    };
        mesh.RecalculateNormals();

        return mesh;
    }

}