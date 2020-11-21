using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private ParticleSystem particleSystem;
    
    // Update is called once per frame
    void Update()
    {
        if (!particleSystem.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
