using UnityEngine;

namespace Players.PowerUps
{
    public class Shield:PowerUp
    {
        private PlayerHealth comp;
        
        

        public override void Act()
        {

            comp=GetComponent<PlayerHealth>();
            comp._canReceiveDamage = false;
            
            comp.powerUpImage.gameObject.SetActive(true);
            comp.powerUpText.gameObject .SetActive(true);
            
            comp.powerUpImage.sprite = logo;
            comp.powerUpText.text = 15 + "s";
            
            Invoke("dest",5);
            
        }

        public override void Dest()
        {
            throw new System.NotImplementedException();
        }

        public void dest()
        {
            
            comp.powerUpImage.gameObject.SetActive(false);
            comp.powerUpText.gameObject.SetActive(false);
            
            comp._canReceiveDamage = true;
            Destroy (this);
        }
    }
}