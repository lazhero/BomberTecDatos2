using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    protected Animator anim;
    protected GameObject body;
    public float velocidad;
    public GameObject currentBlock;
    public GameObject bomba;
    public Map MyMap;

    protected void Start()
    {
        MyMap = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        body = gameObject.transform.GetChild(0).gameObject;
        anim = body.GetComponent<Animator>();
        velocidad = gameObject.GetComponent<Stats>().speed;
    }

    protected void Move(Vector3 dir)
    {

        var x = dir[0]*90 + ((int) (1 - dir[2]) / 2) * 180;

        var angle = Quaternion.Euler(0,x , 0);
        
        anim.SetBool("MOVING", true);
        gameObject.transform.Translate(dir * (velocidad * Time.deltaTime),Space.World);
        body.transform.rotation = angle;

    }

    protected void GenerateBomb()
    {
        GameObject bombita = Instantiate(bomba);
        bombita.transform.position = currentBlock.transform.position + new Vector3(0, 1.5f, 0);
        
    }

}

