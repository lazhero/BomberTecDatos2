using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBombs : MonoBehaviour
{
    public GameObject bomba;
    public Vector3 pos;

    void Update()
    {
        if(Input.GetKeyDown("space")){
            GameObject bombita=Instantiate(bomba);
            bombita.transform.position= pos;
        } 
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.CompareTo("ground")==0)
            pos = other.gameObject.transform.position + new Vector3(0, 1, 0);
    }
}
