using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Timers;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Vector3 = UnityEngine.Vector3;

public class Expansion : MonoBehaviour
{
    private float cubeSize=2.1f;
    private bool finishedCondition;
    
    public int damage = 1;
    
    public Vector3 direction { set; get; }
    public int squares { set; get; } = 4;
    public bool untilTheWall { set; get; }
    [SerializeField] private GameObject explosion;


    private void Update()
    {   
        if(squares>0 && !finishedCondition)
        {
            squares--;
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
            //other.SendMessage("removeHealth",damage);
            other.GetComponent<PlayerHealth>().Health -= 1;
            //Debug.Log("Dano al jugador");
        
        if(other.CompareTag("Bomb"))
            
            other.GetComponent<Bomb>().Explote();
            
            
        if (!other.CompareTag("block")) return;
        
        Instantiate(explosion).transform.position = transform.position;
        Block block = other.GetComponent<Block>();
        if (block.isDestructible)
            block.DestroyMe();
        finishedCondition = true;
    }

  



}

