using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GridMouse2d : MonoBehaviour {
	Color normalColor;
    public Transform selectionObject;

    void Awake() {
        selectionObject = GameObject.Find("HexagonSpriteSelector").transform;
    }

	void Start() {
		normalColor = GetComponent<Renderer>().material.color;
	}

	void Update () {
		//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Ray ray = new Ray(selectionObject.position, Vector3.forward);


		RaycastHit2D hitInfo = Physics2D.Raycast(selectionObject.position, -Vector2.down);

        if (hitInfo.collider != null) {
            //Debug.Log("name:  " + hitInfo.collider.name);
        }

            //Debug.DrawRay(ray.origin, ray.direction, Color.green);
       // if (GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity)) {
		//	GetComponent<Renderer>().material.color = UnityEngine.Color.red;
		//} else {
		//	GetComponent<Renderer>().material.color = normalColor;
		//}			
	}
}
