using UnityEngine;
namespace Players.PowerUps
{
    public class CrossBomb:PowerUp
    {
        
        [SerializeField] private Sprite logo; 
            
        public override void Act()
        {
            comp=GetComponent<PlayerHealth>();
            comp.PreviusbombRatio =comp.BombRatio;
            comp.BombRatio = 100;
            
            comp.powerUpImage.gameObject.SetActive(true);
            comp.powerUpText.gameObject .SetActive(true);
            
            comp.powerUpImage.sprite = logo;
            comp.powerUpText.text = 15 + "s";
            InvokeRepeating("ReduceTime",1,1);
        }


        public override void Dest()
        {
            comp.BombRatio = comp.PreviusbombRatio;
            comp.powerUpImage.gameObject.SetActive(false);
            comp.powerUpText.gameObject .SetActive(false);
            Destroy (this);
            
        }
    }
}