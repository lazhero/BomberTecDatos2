using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBombs : MonoBehaviour
{
    public GameObject bomba;
    public Controller cont;
    public Queue<GameObject> collisions;
    private void Start() {
        cont = transform.parent.GetComponent<Controller>();
       /// collisions= new Queue<GameObject>();
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        if (other.gameObject.tag.CompareTo("ground") != 0) return;
        // if (cont.currentBlock ==null)
       // {
            //var o = transform.parent.gameObject;
            //var position = other.transform.position;
            
            //o.transform.position = new Vector3(position.x, o.transform.position.y, position.z);

            cont.currentBlock = other.gameObject;
        

            //  }

            // if (!collisions.Contains(other.gameObject))
            // collisions.Enqueue(other.gameObject);
    }
    
   
}
