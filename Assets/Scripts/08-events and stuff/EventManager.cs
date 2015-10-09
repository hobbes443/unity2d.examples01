using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {


    public delegate void selectionAction();
    public static event selectionAction selected;
    
    public TileScript hoverObject;
    public enum HoverState{HOVER, NONE};
    public HoverState hover_state = HoverState.NONE;
    

    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity)) {
            
            if (hover_state == HoverState.NONE) {
                // -- not used, but may be useful : 
                // -- hitInfo.collider.SendMessage("OnMouseEnter", SendMessageOptions.DontRequireReceiver);
                // -- hitInfo.transform.gameObject.GetComponent<TileScript>().myName = "yo";
                
                hoverObject = hitInfo.transform.gameObject.GetComponent<TileScript>();
                hoverObject.IsSelected = true;
            }
                
            hover_state = HoverState.HOVER;
            
            selected();
            
        } else {
            if (hover_state == HoverState.HOVER) {
                //if (!hoverObject) {
                    hoverObject.IsSelected = false;
                    hoverObject.SetVisual();
                //}
            }
            ///hoverObject = null;
            hover_state = HoverState.NONE;
        }
        
        
        
    }
 
}