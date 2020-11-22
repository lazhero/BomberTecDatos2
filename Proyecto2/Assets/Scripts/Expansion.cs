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


    public float cubeSize;
    private bool finishedCondition;
    private Vector3 startPosition;


    // Start is called before the first frame update
    void Start()
    {
        cubeSize = gameObject.transform.localScale.x;
        startPosition = gameObject.transform.position;
        StartCoroutine(ExecuteExpansionAfter(0, "FORWARD"));
        StartCoroutine(ExecuteExpansionAfter(10*Time.deltaTime, "BACK"));
        StartCoroutine(ExecuteExpansionAfter(20*Time.deltaTime, "RIGHT"));
        StartCoroutine(ExecuteExpansionAfter(30 * Time.deltaTime, "LEFT"));
        StartCoroutine(DestroyAfter(40 * Time.deltaTime));

    }

    private void Expand(String axis)
    {
        gameObject.transform.position = startPosition;
        gameObject.transform.localScale=new Vector3(cubeSize,cubeSize,cubeSize);
        finishedCondition = false;
        switch (axis)
        {
            case "FORWARD":
                ExpansionVector(cubeSize*Vector3.forward, 3);
                break;
            case "BACK":
                ExpansionVector(cubeSize * Vector3.back, 3);
                break;
            case "RIGHT":
                ExpansionVector(cubeSize*Vector3.right,3);
                break;
            case "LEFT":
                ExpansionVector(cubeSize*Vector3.left,3);
                break;
        }
    }

    public void ExpansionVector(Vector3 vector3, int len)
    {
        if (finishedCondition) return;
        if (len > 0)
        {
            gameObject.transform.position += vector3 / 2;
            Vector3 sizeVector=new Vector3(Math.Abs(vector3.x),Math.Abs(vector3.y),Math.Abs(vector3.z));
            gameObject.transform.localScale += sizeVector;
            ExpansionVector(vector3, len - 1);
        }

    }
    

  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.CompareTo("block")!=0) return;
        Block block = other.GetComponent<Block>();
        if (block.isDestructible) block.DestroyMe();
        else finishedCondition = true;
    }

    IEnumerator ExecuteExpansionAfter(float seconds, string Axis)
    {
        yield return new WaitForSeconds(seconds);
        Expand(Axis);
    }

    IEnumerator DestroyAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

}
