using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Expansion : MonoBehaviour
{


    private float cubeSize;
    private bool finishedCondition;
    [SerializeField]
    private GameObject Destruible;


    // Start is called before the first frame update
    void Start(){
        cubeSize = gameObject.transform.localScale.x;
        finishedCondition = false;
        
    }

    public void ExpansionVector(Vector3 vector3, int len)
    {
        if (finishedCondition) return;
        if (len > 0)
        {
            
            gameObject.transform.position += vector3 / 2;
            gameObject.transform.localScale += vector3;
            ExpansionVector(vector3, len - 1);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherObject = other.gameObject;
        if (otherObject.GetType() == Destruible.GetType())
        {
            Destroy(otherObject);
        }
        else
        {
            finishedCondition = false;
        }
    }
}
