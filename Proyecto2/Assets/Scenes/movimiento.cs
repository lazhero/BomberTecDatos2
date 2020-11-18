using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento : MonoBehaviour
{

    //GameObject
    //gameObject 
    //transform
    public float velocidad;
    public GameObject bomba;

    private BoxCollider micajita;

    private Bomb  miscriptdeexplosion;

    void Start()
    {
        micajita= gameObject.GetComponent<BoxCollider>();
        miscriptdeexplosion= gameObject.GetComponent<Bomb>();

        miscriptdeexplosion.enabled=false;
    }


    void Update()
    {   
        if(Input.GetKey("w")){
            gameObject.transform.position += new Vector3(velocidad,0,0);
        }
        if(Input.GetKey("space")){
            GameObject mibombita = Instantiate(bomba);
            mibombita.transform.position= gameObject.transform.position;
            
        }
        if(Input.GetKey("q")){
            micajita.enabled=false;
        }
        if(Input.GetKey("r")){
            miscriptdeexplosion.enabled=true;
            
        }
    }
}
