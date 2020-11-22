using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossExp : MonoBehaviour
{
    [SerializeField] private GameObject detector;
    [SerializeField] private int len;
    // Start is called before the first frame update
    void OnDestroy()
    {
        startMovement(Vector3.back);
        startMovement(Vector3.forward);
        startMovement(Vector3.right);
        startMovement(Vector3.left);
    }

    void startMovement(Vector3 direction)
    {
        detector.transform.position = gameObject.transform.position;
        GameObject instance = Instantiate(detector);
        Expansion expansion = instance.GetComponent<Expansion>();
        expansion.direction = direction;
        expansion.squares = len;
        expansion.untilTheWall = false;
        expansion.initMovement();
    }
    
}
