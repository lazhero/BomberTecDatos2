using System;
using UnityEngine;

namespace Players.PowerUps
{
    public class CrossBomb:PowerUp
    {
        private PlayerHealth comp;
        public override void Act()
        {
            if(CompareTag("consumable")) return;
            
            comp=GetComponent<PlayerHealth>();
            comp.PreviusbombRatio =comp.BombRatio;
            comp.BombRatio = 10;
            Invoke("dest",5);

        }
        public void dest()
        {
            comp.BombRatio = comp.PreviusbombRatio;
            Destroy (this);
            
        }
    }
}