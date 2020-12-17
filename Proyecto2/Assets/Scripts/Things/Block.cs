using System;
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
        int myPos=Int32.Parse(gameObject.transform.parent.name);
        transform.parent.gameObject.transform.parent.SendMessage("BlockDestroyed",myPos);
        Destroy(gameObject);

    }

    /// <summary>
    /// destroy the block and instantiate debris
    /// </summary>
    public void DestroyMe() {
        if (!isDestructible || isDestroy) return;
        
        GameObject producto= Instantiate(dead_Object);
        producto.transform.position = transform.position;
        
        isDestroy = true;
        Invoke("Destr",0.1f);
        int r = Random.Range(0, 10);
        if (r > 5)
        {
            GameObject myConsumable = Instantiate(consumables[Random.Range(0,consumables.Length)], transform.parent, true);
            myConsumable.transform.position = gameObject.transform.position ;    
        }
        
    }



}