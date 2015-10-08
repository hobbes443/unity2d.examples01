using UnityEngine;
using System.Collections;

[System.Serializable]
public enum SpriteDirection {
    UP    = -90,
    DOWN  = 90,
    LEFT  = 180,
    RIGHT = 0
}

public class FollowMouse : MonoBehaviour {
    public SpriteDirection spriteDirection = SpriteDirection.RIGHT;
        	
	void Update () {
        Vector3 targetPosition = Input.mousePosition;        
        Vector3 turretPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 direction = targetPosition - turretPosition;
        
        transform.rotation = Quaternion.Euler(
            new Vector3(0 , 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + (int)spriteDirection));  
    }
}
