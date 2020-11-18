using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBombs : MonoBehaviour
{
    public GameObject bomba;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")){
            GameObject bombita=Instantiate(bomba);
            bombita.transform.position= transform.position;
        } 
    }
}
