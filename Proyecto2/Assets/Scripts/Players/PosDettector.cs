using System;
using System.Collections;
using System.Collections.Generic;
using Things;
using UnityEngine;

public class PosDettector : MonoBehaviour
{
    public GameObject bomba;
    public Controller cont;
    public Queue<GameObject> collisions;
    private void Start() {
        cont = transform.parent.GetComponent<Controller>();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.CompareTo("ground") != 0) return;

        var pastBlock = cont.currentBlock;
        if (pastBlock != null)
            cont.myMap.ThingChange(new message("itemOrPlayer",Convert.ToInt32(cont.currentBlock.name), message.Erase));
        
        cont.currentBlock = other.gameObject;
        cont.myMap.ThingChange(new message("itemOrPlayer",Convert.ToInt32(cont.currentBlock.name), message.Write));

           
    }
    
   
}
