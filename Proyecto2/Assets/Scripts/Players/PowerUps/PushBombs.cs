using UnityEngine;
using UnityEngine.UI;

namespace Players.PowerUps
{
    public class PushBombs : PowerUp
    {
        private Transform forward;
        [SerializeField] private Sprite logo; 

        
        public override void Act()
        {
            if(CompareTag("consumable")) return;

            comp=GetComponent<PlayerHealth>();
            forward = transform.GetChild(0).transform;
            
            comp.powerUpImage.gameObject.SetActive(true);
            comp.powerUpText.gameObject .SetActive(true);

            comp.powerUpImage.sprite = logo;
            comp.powerUpText.text = 15 + "s";
            InvokeRepeating("ReduceTime",1,1);
            
        }

        public override void Dest()
        {
            comp.powerUpImage.gameObject.SetActive(false);
            comp.powerUpText.gameObject.SetActive(false);
            Destroy (this);
        }

        private void OnCollisionEnter(Collision other)
        {

            if(CompareTag("Player")||CompareTag("Enemy"))
            {
                if(other.gameObject.CompareTag("Bomb"))
                {
                    var i = other.gameObject.GetComponent<Rigidbody>();
                    i.AddForce(forward.forward * 10, ForceMode.Impulse);
                    i.constraints = RigidbodyConstraints.None;
                    i.constraints = RigidbodyConstraints.FreezeRotationY;
                }
            }
        }

  
    }
}