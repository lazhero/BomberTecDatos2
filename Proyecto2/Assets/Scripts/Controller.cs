using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Controller:MonoBehaviour
{
    protected Animator anim;
    protected GameObject body;
    public float velocidad= 0.5f;
    protected void Start()
    {
       
        body=gameObject.transform.GetChild(0).gameObject;
        anim=body.GetComponent<Animator>();
    }
    protected void Move(Vector3 dir,Quaternion angle ){
        anim.SetBool("MOVING",true);
        gameObject.transform.Translate(dir * (velocidad * Time.deltaTime));
        body.transform.rotation= angle;

    }
}
