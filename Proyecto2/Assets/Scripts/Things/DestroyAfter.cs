﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    [SerializeField]
    private float Time= 1.5f;
    void Start()
    {
        Invoke("Destr",Time);
    }

    /// <summary>
    ///  Destroy GameObject
    /// </summary>
    void Destr()
    {
        Destroy(gameObject);
    }
}
