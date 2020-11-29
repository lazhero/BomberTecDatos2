using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public int healthRestoration = 1;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Adding Health");
            other.SendMessageUpwards("AddHealth", healthRestoration);
            _collider.enabled = false;
            Destroy(gameObject);
        }
    }
}
