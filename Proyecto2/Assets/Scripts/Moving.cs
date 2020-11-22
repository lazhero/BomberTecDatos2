using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    Animator anim;
    GameObject body;
    public float velocidad= 0.5f;
    void Start(){
        body=gameObject.transform.GetChild(0).gameObject;
        anim=body.GetComponent<Animator>();
    }

    void Move(Vector3 dir,Quaternion angle ){
            anim.SetBool("MOVING",true);
            gameObject.transform.Translate(dir * (velocidad * Time.deltaTime));
            body.transform.rotation= angle;

    }
    /// <summary>
    /// Dettects if the player has pressed one key
    /// </summary>
    void KeyPulsation()
    {
        if     (Input.GetKey("w"))Move(-Vector3.forward,Quaternion.Euler(0, 180,0));
        else if(Input.GetKey("s"))Move(Vector3.forward,Quaternion.Euler (0,  0, 0));
        else if(Input.GetKey("d"))Move(-Vector3.right,Quaternion.Euler  (0, -90,0));
        else if(Input.GetKey("a"))Move(Vector3.right,Quaternion.Euler   (0,  90,0));
        else anim.SetBool("MOVING",false);
    }
    
    void Update(){   

        KeyPulsation();
    }
}
