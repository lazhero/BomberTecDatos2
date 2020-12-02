﻿using UnityEngine;

public class Block : MonoBehaviour
{

    [SerializeField]
    public bool isDestructible;
    
    [SerializeField] private GameObject dead_Object;
    private bool isDestroy=false;


    /// <summary>
    /// destroy the block 
    /// </summary>
    void Destr() {
        Destroy(gameObject);
    }

    /// <summary>
    /// destroy the block and instantiate debris
    /// </summary>
    public void DestroyMe() {
        if (!isDestructible || isDestroy) return;
        
        isDestroy = true;
        var producto= Instantiate(dead_Object);
        producto.transform.position = transform.position;
                    
        Invoke("Destr",0.1f);

    }

}
