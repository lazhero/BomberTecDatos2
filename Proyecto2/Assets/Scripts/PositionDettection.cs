using System;
using UnityEngine;

public class PositionDettection : MonoBehaviour
{
    private Map Map;
    private void Start()
    {
        Map = gameObject.transform.parent.gameObject.GetComponent<Map>();
    }

    private void OnCollisionEnter(Collision other) {
        //Debug.Log(gameObject.name);
        //float[] algo= Map.Graph.GetRelations(Convert.ToInt32(gameObject.name));
       // Debug.Log("vecinos"+algo[0]+" "+algo[1]+" "+algo[2]+" "+algo[3] );
    }
}
