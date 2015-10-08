using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class GridTile : MonoBehaviour {

	public Sprite selectionHex;
	  
	public Vector2 myGridPosition;
	public bool IsSelected = false;
	
	private SpriteRenderer spriteRenderer;
	private Sprite defaultSprite;
	
	private GridMap gridMap;


	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		defaultSprite = spriteRenderer.sprite;
		
		if (GameObject.Find("HexagonGrid") != null) {
			gridMap = GameObject.Find("HexagonGrid").GetComponent<GridMap>();
		}
		
			
	}

	void OnMouseOver() {
		this.IsSelected = true;
		
		List<GridTile> adjecentTiles = gridMap.GetAdjacentTiles(myGridPosition);
		
		foreach (GridTile gt in adjecentTiles) {
			gt.IsSelected = true;
			gt.SetVisual();
		}
	}
	void OnMouseExit() {
		this.IsSelected = false;
		
		List<GridTile> adjecentTiles = gridMap.GetAdjacentTiles(myGridPosition);
		
		foreach (GridTile gt in adjecentTiles) {
			gt.IsSelected = false;
			gt.SetVisual();
		}		
	}
	
	void Update() {
		SetVisual();
	}
	
	void OnCollisionEnter(Collision collision) {
		Debug.Log("yo collision in gridtile??");
	}

	void Updatetest () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);		
		RaycastHit hitInfo;
					
		if (GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity)) {
            //Debug.Log("yup");
			//spriteRenderer.sprite = selectionHex;
			this.IsSelected = true;
			//SetVisual(spriteRenderer);
			
			//gridMap.Test(myGridPosition);
		} else {
			this.IsSelected = false;
			//SetVisual(spriteRenderer);
			//spriteRenderer.sprite = defaultSprite;
		}
		
		SetVisual();
		
	}
	
	public void Test() {
		//Debug.Log("from tile test: " + myGridPosition.ToString());
	}
	
	
	public void SetVisual() {
		//if (gridMap.GetAdjacent(new Vector2(2,3)) != null) Debug.Log("made it");

		if (IsSelected) {
			spriteRenderer.sprite = selectionHex;
		} else {
			spriteRenderer.sprite = defaultSprite;
		}
		
	}
	
}
