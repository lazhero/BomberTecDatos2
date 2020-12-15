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


            Debug.Log("voy a buscar :" +Focus);
            var targets = myMap.Things[Focus];
            if(targets.Count<0) return;
            var Target = Random.Range(0, targets.Count);
            controller.AddMovement(targets[Target]);
            
        }
        
    }
}