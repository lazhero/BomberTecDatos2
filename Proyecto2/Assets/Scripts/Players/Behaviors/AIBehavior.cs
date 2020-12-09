using UnityEngine;

namespace Players.Behaviors
{
    public abstract class AiBehavior: MonoBehaviour
    {
        public  IAMovementController controller { set; get; }
        public  Map myMap ;
        public abstract void Act();
        private void Start()
        {
            controller = GetComponent<IAMovementController>();
            myMap = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        }
    }
}