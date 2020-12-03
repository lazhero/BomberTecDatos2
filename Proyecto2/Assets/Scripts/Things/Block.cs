﻿using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{

    public GameObject[] consumables;

    [SerializeField]
    public bool isDestructible;

    [SerializeField] private GameObject dead_Object;
    private bool isDestroy=false;



    /// <summary>
    /// destroy the block 
    /// </summary>
    void Destr() {
        Destroy(gameObject);
        int myPos=Int32.Parse(gameObject.transform.parent.name);
        transform.parent.gameObject.transform.parent.SendMessage("BlockDestroyed",myPos);
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
        GameObject myConsumable = Instantiate(consumables[Random.Range(0,consumables.Length)]);
        myConsumable.transform.position = gameObject.transform.position ;
        //! Debug.Log("Consumable created");
    }



}