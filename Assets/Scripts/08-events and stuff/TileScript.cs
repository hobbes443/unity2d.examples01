using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {

    public string myName = "first";
    public Sprite selectionHex;


    void OnEnable() {
        EventManager.selected += ImSelected;
    }

    void OnDisable() {
        EventManager.selected -= ImSelected;
    }


    void ImSelected() {
        Debug.Log(myName);
    }



}
