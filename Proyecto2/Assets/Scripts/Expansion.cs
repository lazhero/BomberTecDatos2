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


    private float cubeSize;
    private bool finishedCondition;
    
    public Vector3 direction { set; get; }
    public int squares { set; get; }
    public bool untilTheWall { set; get; }

    // Start is called before the first frame update
    void Start()
    {
        cubeSize = gameObject.transform.localScale.x;
        direction = direction * cubeSize;
        finishedCondition = false;
    }

    public void initMovement()
    {
        ExpansionVector();
    }
    
    void ExpansionVector()
     { 
         if (finishedCondition) return; 
         if (squares > 0 || untilTheWall)
         {
            gameObject.transform.position += direction/2;
            gameObject.transform.localScale+=new Vector3(Math.Abs(direction.x),Math.Abs(direction.y),Math.Abs(direction.z));
            squares--;
            Invoke("ExpansionVector",10*Time.deltaTime);
         }

     }
    

  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.CompareTo("block")!=0) return;
        Block block = other.GetComponent<Block>();
        if (block.isDestructible) block.DestroyMe();
        else finishedCondition = true;
    }

  



}
