using System;
using System.Collections;
using System.Collections.Generic;
using Players;
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
        _collider.enabled = false;
        Invoke("activateCollider",0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var info = other.gameObject.GetComponent<PlayerHealth>();
        
        info.ModifyStats(healthRestoration,shield,bomb,shoe);
        
        Destroy(gameObject);
        
    }

    public void activateCollider()
    {
        _collider.enabled = true;
    }
    public void Disapear()
    {
        Destroy(gameObject);
    }
}
