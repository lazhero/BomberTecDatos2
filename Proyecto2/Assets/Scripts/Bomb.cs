﻿﻿using System;
  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosion;

    public float time;
    public BoxCollider bc;
    void Start()
    {
        bc = GetComponent<BoxCollider>();
        Invoke("Explote",time);
        bc.enabled = false;
    }
    /// <summary>
    ///  Destroy gameObject and generates the explosion
    /// </summary>
    public void Explote(){
        GameObject exp=Instantiate(explosion);
        exp.transform.position= transform.position;
        Destroy(gameObject);

    }

    private void OnTriggerExit(Collider other)
    {
        bc.enabled = true;

    }
}
