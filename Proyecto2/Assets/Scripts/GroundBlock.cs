using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBlock : MonoBehaviour
{

    public Block block;
    public GameObject blockObject;
  

    public void Reset()
    {    
        if(blockObject??false)
            Destroy(blockObject);
    }

  
}
