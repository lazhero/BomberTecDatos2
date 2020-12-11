using Players.Behaviors;
using UnityEngine;

namespace Players
{
    public class LeaveBomb:AiBehavior
    {
        private bool bombLaunched;
        private AiBehavior myBehaviour;
       
        
        public override void Act()
        {
            bombLaunched = false;
            controller.GenerateBomb();
            myBehaviour=gameObject.GetComponent<Hide>();
            Invoke("RunAway",0.5f);
            
        }

        private void RunAway()
        {
            bombLaunched = true;
            myBehaviour.Act();
        }
        public override bool stillActive()
        {
            if (!bombLaunched) return true;
            return myBehaviour.stillActive();
        }
        
    }
}