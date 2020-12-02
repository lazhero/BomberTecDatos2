using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionDetection : MonoBehaviour
{
    public GameObject bomba;
    public Controller cont;
    public Queue<GameObject> collisions;
    private void Start() {
        cont = transform.parent.GetComponent<Controller>();
    }


    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("enter");
        if (other.gameObject.tag.CompareTo("ground") != 0) return;
        cont.currentBlock = other.gameObject;
        
    }
    
   
}
