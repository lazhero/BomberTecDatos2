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

        if (xaxis > 0 || yaxis> 0)
        {
            Vector3 movement= new Vector3();
            if (Mathf.Abs(xaxis)  > Mathf.Abs(yaxis))
            {
                
            }
        }

        if (Input.GetButton("pbomb"))
        {
            
        }
        if(Input.GetKey("w"))    Move(-Vector3.forward);
        else if(Input.GetKey("s"))Move(Vector3.forward);
        else if(Input.GetKey("d"))Move(-Vector3.right );
        else if(Input.GetKey("a"))Move(Vector3.right) ;
        else setMoving(false);
        if (Input.GetKeyDown("space")) GenerateBomb();
        
    }
    public void setMoving(bool value)
    {
        Anim.SetBool("MOVING",value);
    }
}
