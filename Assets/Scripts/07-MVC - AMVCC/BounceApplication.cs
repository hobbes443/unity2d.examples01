using UnityEngine;


// all of this is based on the tutorial found here:
//  http://www.toptal.com/unity-unity3d/unity-with-mvc-how-to-level-up-your-game-development


// Base class for all elements in this application.
public class BounceElement : MonoBehaviour {
	//Singleton?? better way?
	//--Gives access to the application and all instances.
   public BounceApplication app { get { return GameObject.FindObjectOfType<BounceApplication>(); }}
}


public class BounceApplication : MonoBehaviour {

   // Reference to the root instances of the MVC.
   public BounceModel model;
   public BounceView view;
   public BounceController controller;

   // Init things here
   void Start() { }
   
   // Iterates all Controllers and delegates the notification data
   // This method can easily be found because every class is “BounceElement” and has an “app” 
   // instance.
   public void Notify(string p_event_path, Object p_target, params object[] p_data)
   {
      BounceController[] controller_list = GetAllControllers();
      foreach(BounceController c in controller_list)
      {
         c.OnNotification(p_event_path,p_target,p_data);
      }
   }
   
   public BounceController[] GetAllControllers() { 
       
       return GameObject.FindObjectsOfType<BounceController>();
       
   }
   
}
