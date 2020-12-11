using System;
using System.Collections;
using System.Collections.Generic;
using Players;
using Things;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public int healthRestoration = 1;
    public int shoe = 1;
    public int bomb = 1;
    public int shield = 1;
    private Map mymap;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = false;

        var currentBlock = transform.parent;
        
        mymap = currentBlock.transform.parent.GetComponent<Map>();
        mymap.ThingChange(new message("item",Convert.ToInt32(currentBlock.name), message.Write));

        
        Invoke("activateCollider",0.5f);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Enemy")) return;

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
        var currentBlock = transform.parent;
        mymap = currentBlock.transform.parent.GetComponent<Map>();
        mymap.ThingChange(new message("item",Convert.ToInt32(currentBlock.name), message.Erase));
        Destroy(gameObject);
        
    }
}
