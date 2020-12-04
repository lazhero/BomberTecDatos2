using UnityEngine;

namespace Players
{
    public abstract class Behavior
    {
       public  IAMovementController controller { set; get; }
        public  Map myMap { set; get; }
        public abstract void Act();
    }
}