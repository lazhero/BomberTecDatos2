using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Timers;
using Players;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Vector3 = UnityEngine.Vector3;

public class Expansion : MonoBehaviour
{
    private float cubeSize=2.1f;
    private bool finishedCondition;
    
    public int damage = 1;
    
    public Vector3 direction { set; get; }
    public int ratio { set; get; } = 4;
    public bool untilTheWall { set; get; }
    [SerializeField] private GameObject explosion;


    private void Update()
    {   
        if(ratio>0 && !finishedCondition)
        {
            ratio--;
            transform.Translate(Vector3.forward + Vector3.forward * (cubeSize * Time.deltaTime));
            Instantiate(explosion).transform.position = transform.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }

 
    

  
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerHealth>().Health -= 1;
        
        if(other.CompareTag("Bomb"))
            other.GetComponent<Bomb>().Explote();
            
        if(other.CompareTag("consumable"))
            other.GetComponent<Consumable>().Disapear();

        if (!other.CompareTag("block")) return;
        
        Instantiate(explosion).transform.position = transform.position;
        Block block = other.GetComponent<Block>();
        if (block.isDestructible)
            block.DestroyMe();
        finishedCondition = true;
    }

  



}

