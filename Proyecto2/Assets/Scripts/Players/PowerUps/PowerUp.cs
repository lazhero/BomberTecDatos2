using TMPro;
using UnityEngine;

namespace Players.PowerUps
{
    public abstract class PowerUp: MonoBehaviour
    {
        public  Controller controller { set; get; }
        public abstract void Act();
        public abstract void Dest();
        protected PlayerHealth comp;
        public int time=15;

        public void Start()
        {
            controller = GetComponent<Controller>();
            Act();
        }
        private void setTime()
        {
            time--;
        }
        public void ReduceTime()
        {
            if(time<=0)
                Dest();
            time--;
            comp.powerUpText.text = time.ToString()+ "s";
            
            
        }

      
   
        
        
    }
}