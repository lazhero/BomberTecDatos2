using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public int healthRestoration = 1;
    public int shoe = 1;
    public int bomb = 1;
    public int shield = 1;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.tag== "Ham")
            {
                other.SendMessage("AddHealth",healthRestoration);
                _collider.enabled = false;
                Destroy(gameObject);
                return;
            }
        
            if (this.tag == "Shield")
            {
                other.SendMessage("AddShield",shield);
                _collider.enabled = false;
                Destroy(gameObject);
                return;
            }
            
            if (this.tag == "Shoe")
            {
                other.SendMessage("AddShoe",shoe);
                _collider.enabled = false;
                Destroy(gameObject);
                return;
            }
            
            if (this.tag == "Bomb")
            {
                other.SendMessage("AddBomb",bomb);
                _collider.enabled = false;
                Destroy(gameObject);
                return;
            }
        }
    }
}
