using System;
using System.Collections;
using System.Collections.Generic;
using Players;
using Players.PowerUps;
using Things;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    
    private Map mymap;
    private Collider _collider;
    private  Component action;
    public bool desactiveOnAwake;
    
    private void Start()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = desactiveOnAwake;


        action = GetComponent<PowerUp>();
        var currentBlock = transform.parent;
        
        mymap = currentBlock.transform.parent.GetComponent<Map>();
        mymap.ThingChange(new message("item",Convert.ToInt32(currentBlock.name), message.Write));

        
        Invoke("activateCollider",0.5f);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Enemy")) return;
        
        if (other.gameObject.GetComponent<PowerUp>() == null)
        {
#if UNITY_EDITOR
            UnityEditorInternal.ComponentUtility.CopyComponent(action);
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(other.gameObject);
#endif
        }
        GameObject.FindObjectOfType<Mendel>().updateValue(Int32.Parse(other.name),30 );  
        /*
        var info = other.gameObject.GetComponent<PlayerHealth>();
        info.ModifyStats(healthRestoration,shield,bomb,shoe);
        */
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
