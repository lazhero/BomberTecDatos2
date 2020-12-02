using System;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    private GameObject toGenerate;

    private void Update()
    {
        GameObject gameObject;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            gameObject=Instantiate(toGenerate);
            gameObject.AddComponent<PlayerController>();
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            gameObject=Instantiate(toGenerate);
            gameObject.AddComponent<IAMovementController>();
        }

    }
}