using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Players.Behaviors
{
    
    public class Follow:AiBehavior
    {

        public string Focus;
        public override void Act()
        {
            
            FollowTarget();
        }

        protected void FollowTarget( )
        {
            
            var targets = myMap.Things[Focus];
            //if(targets.Count<0) return;
            var Target = Random.Range(0, targets.Count-1);
            
            controller.AddMovement(targets[Target]);
            
        }
        
    }
}