using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {

    public string myName = "first";
    public Sprite selectionHex;
    private Sprite defaultSprite;
    
    public bool IsSelected = false;
    
    void Awake() {
        defaultSprite = GetComponent<SpriteRenderer>().sprite;
    }


    void OnEnable() {
        EventManager.selected += ImSelected;
    }

    void OnDisable() {
        EventManager.selected -= ImSelected;
    }


    void ImSelected() {
        
        if (IsSelected) Debug.Log(myName);
        
        //GetComponent<SpriteRenderer>().sprite = selectionHex;
        SetVisual();
        
    }



    public void SetVisual() {
		//if (gridMap.GetAdjacent(new Vector2(2,3)) != null) Debug.Log("made it");

		if (IsSelected) {
			GetComponent<SpriteRenderer>().sprite = selectionHex;
		} else {
			GetComponent<SpriteRenderer>().sprite = defaultSprite;
		}
		
	}


}
