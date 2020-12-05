using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mendel : MonoBehaviour
{
    public GameObject[] Beans { set; get; }

    public void AddNewBean(GameObject player)
    {
        for (int iterator = 0; iterator < Beans.Length; iterator++)
        {
            if (Beans[iterator] == null)
            {
                Beans[iterator] = player;
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
