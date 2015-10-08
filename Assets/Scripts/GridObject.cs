using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GridObject : MonoBehaviour {
	Color normalColor;
	GameObject[] selectors;
    //public Transform selectionObject;

    void Awake() {
		selectors = GameObject.FindGameObjectsWithTag("selectortile");
        //selectionObject = GameObject.Find("HexagonSelectorTile").transform;
    }

	void Start() {
		normalColor = GetComponent<Renderer>().material.color;
	}

	void Update () {
		foreach (GameObject selector in selectors) {
			//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//Ray ray = new Ray(selectionObject.position, Vector3.forward);
			Ray ray = new Ray(selector.transform.position, Vector3.forward);
			RaycastHit hitInfo;
	
			Debug.DrawRay(ray.origin, ray.direction, Color.green);
			if (GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity)) {
				GetComponent<Renderer>().material.color = UnityEngine.Color.red;
			} else {
				GetComponent<Renderer>().material.color = normalColor;
			}			
		}
	}
}
