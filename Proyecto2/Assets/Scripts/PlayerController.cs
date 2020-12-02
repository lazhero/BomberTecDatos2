using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController:Controller
{
    void KeyPulsation()
    {
        if(Input.GetKey("w"))    Move(-Vector3.forward);
        else if(Input.GetKey("s"))Move(Vector3.forward);
        else if(Input.GetKey("d"))Move(-Vector3.right );
        else if(Input.GetKey("a"))Move(Vector3.right) ;
        else setMoving(false);
        if (Input.GetKeyDown("space")) GenerateBomb();
    }
    
    void Update(){   

        KeyPulsation();
    }

    public void setMoving(bool value)
    {
        anim.SetBool("MOVING",value);
    }
}
