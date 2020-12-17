using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : Controller
{
   
    void Update()
    {

        keyPulsation();
            
    }

    void keyPulsation()
    {
        float xaxis = Input.GetAxis("MHorizontal");
        float yaxis = Input.GetAxis("MVertical");

        if (xaxis != 0 || yaxis!= 0)
        {
            if (Mathf.Abs(xaxis)  > Mathf.Abs(yaxis))
                Move(-Vector3.right *Mathf.Sign(xaxis));
            else
                Move(Vector3.forward *Mathf.Sign(yaxis));
        }
        else
        {
            setMoving(false);
        }
        if (Input.GetButton("pbomb"))
        {
            GenerateBomb();
        }

        
    }
    public void setMoving(bool value)
    {
        Anim.SetBool("MOVING",value);
    }
}
