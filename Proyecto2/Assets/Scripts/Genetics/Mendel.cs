using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mendel : MonoBehaviour
{
    public GameObject[] Being { set; get; }

    public void AddNewBean(GameObject player)
    {
        for (int iterator = 0; iterator < Being.Length; iterator++)
        {
            if (Being[iterator] == null)
            {
                Being[iterator] = player;
            }
        }
    }
    
    
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
