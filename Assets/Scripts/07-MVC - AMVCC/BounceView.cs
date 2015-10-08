using UnityEngine;
using System.Collections;

public class BounceView : BounceElement {

   // Reference to the ball
   public BallView ball;
   
   
   
   // Only this is necessary. Physics is doing the rest of work.
   // Callback called upon collision.
   void OnCollisionEnter() { app.Notify(BounceNotifications.BallHitGround,this); }   
   
}
