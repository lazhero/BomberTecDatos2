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
    public void Explote(){
        GameObject exp=Instantiate(explosion);
        exp.transform.position= transform.position;
        Destroy(gameObject);
    }
}
