using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionDettection : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        Debug.Log(gameObject.name);
    }
}
