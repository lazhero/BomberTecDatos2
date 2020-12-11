using UnityEngine;
using UnityEngine.UIElements;

namespace Players.Behaviors
{
    public abstract class AiBehavior: MonoBehaviour
    {
        public  IAMovementController controller { set; get; }
        public  Map myMap ;
        public abstract void Act();
        public void Start()
        {
            controller = GetComponent<IAMovementController>();
            myMap = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        }

        public virtual bool stillActive()
        {
            return controller.Ismoving();
        }
        
        
    }
}