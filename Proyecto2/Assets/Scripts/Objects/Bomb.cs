using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosion;

    public float time;
    void Start(){
        Invoke("Explote",time);
    }
    /// <summary>
    ///  Destroy gameObject and generates the explosion
    /// </summary>
    public void Explote(){
        GameObject exp=Instantiate(explosion);
        exp.transform.position= transform.position;
        Destroy(gameObject);

    }
}
