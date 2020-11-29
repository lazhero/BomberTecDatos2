using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController:Controller
{
    void KeyPulsation()
    {
        if(Input.GetKey("w"))Move(-Vector3.forward,Quaternion.Euler(0, 180,0));
        else if(Input.GetKey("s"))Move(Vector3.forward,Quaternion.Euler (0,  0, 0));
        else if(Input.GetKey("d"))Move(-Vector3.right,Quaternion.Euler  (0, -90,0));
        else if(Input.GetKey("a"))Move(Vector3.right,Quaternion.Euler   (0,  90,0));
        else setMoving(false);
        if (Input.GetKeyDown("space")) GenerateBomb();
        
        if (Input.GetKey("h")) GenerateHam();
        
    }
    
    void Update(){   

        KeyPulsation();
    }

    public void setMoving(bool value)
    {
        anim.SetBool("MOVING",value);
    }
}
