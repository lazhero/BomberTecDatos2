using UnityEngine;

namespace Players.PowerUps
{
    public abstract class PowerUp: MonoBehaviour
    {
        public  Controller controller { set; get; }
        public abstract void Act();
        public void Start()
        {
            controller = GetComponent<Controller>();
            Act();
        }

   
        
        
    }
}