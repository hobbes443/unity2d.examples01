using UnityEngine;

public class BounceController : BounceElement {

   // Handles the ball hit event
   public void OnBallGroundHit()
   {
      app.model.bounces++;
      Debug.Log("Bounce " + app.model.bounces);
      if(app.model.bounces >= app.model.winCondition)
      {
         app.view.ball.enabled = false;
         app.view.ball.GetComponent<Rigidbody>().isKinematic=true; // stops the ball
         OnGameComplete();
      }	
   }

   // Handles the ball hit event
   public void OnNotification(string p_event_path,Object p_target,params object[] p_data)
   {
      switch(p_event_path)
      {
         case BounceNotifications.BallHitGround: 
            app.model.bounces++;
            Debug.Log("Bounce "+app.model.bounces);
            if(app.model.bounces >= app.model.winCondition)
            {
               app.view.ball.enabled = false;
               app.view.ball.GetComponent<Rigidbody>().isKinematic=true; // stops the ball
               // Notify itself and other controllers possibly interested in the event
               app.Notify(BounceNotifications.GameComplete,this);            
            }
         break;
         
         case BounceNotifications.GameComplete:
            Debug.Log("Victory!!");
         break;
      }	
   }

   // Handles the win condition
   public void OnGameComplete() { Debug.Log("Victory!!"); }
   
}
