using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionCamara : MonoBehaviour
{   

    void Update()
    {
        
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            transform.position += new Vector3(0,1,0);
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            transform.position += new Vector3(0,-1,0);
                    
        }


    }

}
