using UnityEngine;
using System.Collections;

public class BallView : BounceElement {

   // Only this is necessary. Physics is doing the rest of work.
   // Callback called upon collision.
   void OnCollisionEnter() { app.controller.OnBallGroundHit(); }
   
   
}
