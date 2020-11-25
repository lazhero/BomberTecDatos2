using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBombs : MonoBehaviour
{
    public GameObject bomba;
    public Controller cont;
    public Queue<string> collisions;
    private void Start() {
        cont = transform.parent.GetComponent<Controller>();
        collisions= new Queue<string>();
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        if (other.gameObject.tag.CompareTo("ground") != 0) return;
        
        if (cont.currentBlock ==null)
        {
            var o = transform.parent.gameObject;
            var position = other.transform.position;
            
            o.transform.position = new Vector3(position.x, o.transform.position.y, position.z);

            cont.currentBlock = other.gameObject;
        }
        
        if (!collisions.Contains(other.name))
            collisions.Enqueue(other.name);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit");
        if (other.gameObject.tag.CompareTo("ground")==0)
            collisions.Dequeue();
    }

    private void OnTriggerStay(Collider other)
    {
        var X1 = transform.position.x;
        var Z1 = transform.position.z;
        
        var X2 = other.transform.position.x;
        var Z2 = other.transform.position.z;


        var min = 0.1f;
        var X_distance_Range = X1 < X2 + min  && X1 > X2 - min;
        var Y_distance_Range = Z1 < Z2 + min  && Z1 > Z2 - min;

        if (other.gameObject.tag.CompareTo("ground") != 0 || collisions.Count != 1) return;
        
        if(X_distance_Range&&Y_distance_Range)
            cont.currentBlock = other.gameObject;
    }

   
}
