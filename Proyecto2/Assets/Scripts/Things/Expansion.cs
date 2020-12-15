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
    private float cubeSize=1.8f;
    private bool finishedCondition;
    
    public int damage = 1;
    public string Owner { set; get; }
    public Vector3 direction { set; get; }
    public int ratio { set; get; } = 4;
    public bool untilTheWall { set; get; }
    [SerializeField] private GameObject explosion;
    private Mendel mendel;

    private void Start()
    {
        mendel = GameObject.FindObjectOfType<Mendel>();
    }

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
        int value = 0;
        
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            other.GetComponent<PlayerHealth>().Health -= 1;
            if (other.name.CompareTo(Owner) != 0)
            {
                value = 50;
                mendel.updateValue(Int32.Parse(other.gameObject.name),-value );
            }
            else value = -50;
            
        }
        

        if (other.CompareTag("consumable"))
        {
            other.GetComponent<Consumable>().Disapear();
            value = -10;
        }
           

        if (other.CompareTag("block"))
        {
            Block block = other.GetComponent<Block>();
            if (block.isDestructible)
            {
                value = 50;
                block.DestroyMe();
            }
        }
        mendel.updateValue(Int32.Parse(Owner),value);
        Instantiate(explosion).transform.position = transform.position;
        finishedCondition = true;
    }

  



}

