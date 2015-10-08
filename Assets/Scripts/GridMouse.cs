using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GridMouse : MonoBehaviour {
	
	Color normalColor;

	void Start() {
		normalColor = GetComponent<Renderer>().material.color;
	}

	void Update () {

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				
		RaycastHit hitInfo;
					
		if (GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity)) {
            Debug.Log("yup");
			
			GetComponent<Renderer>().material.color = UnityEngine.Color.red;

			
		} else {
			GetComponent<Renderer>().material.color = normalColor;
		}
		
	}
}
