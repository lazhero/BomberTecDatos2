using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Players;
using UnityEngine;
using Random = UnityEngine.Random;
public class IAProbability : MonoBehaviour
{
    private float[] probabilities;
    private Behavior[] behaviors;
    private IAMovementController movement;
    
    private void Start()
    {
        movement = gameObject.GetComponent<IAMovementController>() ;
        
    }

    public void setBehaviorsNumber(int number)
    {
        probabilities = new float[number];
        behaviors=new Behavior[number];
    }

    public void addBehavior(Behavior behavior)
    {
        behavior.controller = movement;
        bool reached = false;
        for (int i = 0; i < behaviors.Length && !reached; i++)
        {
            if (behaviors[i] == null)
            {
                behaviors[i] = behavior;
                reached = true;
            }
        }
        
        

    }



    public void RandomAction()
    {
        float randomNumber = Random.Range(0, 100);
        int i = 0;
        while (randomNumber > 0)
        {
            if (randomNumber < probabilities[i])
            {
                behaviors[i].Act();
            }
            randomNumber -= probabilities[i];
            i++;
        }
        
    }
    
    void Update()
    {
        
    }
}
