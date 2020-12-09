using Random = UnityEngine.Random;

namespace Players.Behaviors
{
    
    public class Follow:AiBehavior
    {
  

        public override void Act()
        {
            FollowTarget();
        }

        private void FollowTarget( )
        {
            var targets = myMap.Things["itemOrPlayer"];
            var Target = Random.Range(0, targets.Count);
            controller.AddMovement(targets[Target]);

        }


        
    }
}