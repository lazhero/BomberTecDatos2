using System;
using UnityEngine;

namespace Players.PowerUps
{
    public class PushBombs : PowerUp
    {
        private Transform forward;
        public override void Act()
        {
            if(CompareTag("consumable")) return;

            forward = transform.GetChild(0).transform;
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