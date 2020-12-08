using UnityEngine;

namespace Players.Behaviors
{
    public abstract class AiBehavior: MonoBehaviour
    {
        public  IAMovementController controller { set; get; }
        public  Map myMap { set; get; }
        public abstract void Act();
        private void Start()
        {
            controller = GetComponent<IAMovementController>();
        }
    }
}