using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {


    public delegate void selectionAction();
    public static event selectionAction selected;


    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity)) {
            //hitInfo.transform.gameObject.GetComponent<TileScript>().myName = "yo";

            selected();

        }



    }
}
