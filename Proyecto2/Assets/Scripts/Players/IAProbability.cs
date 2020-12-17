using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DataStructures;
using Players.Behaviors;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class IAProbability : MonoBehaviour
{
    [SerializeField] public float[] probabilities;
    [SerializeField] public AiBehavior[] behaviors { set; get; }
    [SerializeField] public float time { set; get; } 

    private IAMovementController movement;
    private AiBehavior current;
    private double timeLeft;
    private bool launched;


    private void Start()
    {
        time = 20000 * Time.deltaTime;
        movement = gameObject.GetComponent<IAMovementController>();
        behaviors = GetComponents<AiBehavior>();
        launched = false;
        StartCoroutine(pause());
        

    }

 


    void Update()
    {
        if (timeLeft <= 0 || !current.stillActive() ||current==null)
        {
            if (!launched)
            {
                launched = true;
                movement.emptyMovement();
                StartCoroutine(pause()); 
                
            }
            
        }
        else
        {
            timeLeft -= Time.deltaTime;
        }



    }

    public int getNumberOfActions()
    {
        return probabilities.Length;
    }
    
    public void setBehaviorsNumber(int number)
    {
        probabilities = new float[number];
        behaviors = new AiBehavior[number];
        float initialValue = 100 / number;
        for (int i = 0; i < probabilities.Length; i++)
            probabilities[i] = initialValue;


    }

    /// <summary>
    /// this is only for test
    /// </summary>
    public void SortProbabilities()
    {
        probabilities = Sorter.BubbleSort(probabilities);
    }
    
    public void RandomAction()
    {
        float randomNumber = Random.Range(0, 100)%100;
        int i = 0;
        while (randomNumber > 0 && i<probabilities.Length)
        {
            if (randomNumber <= probabilities[i])
            {
                current = behaviors[i];
                current.Act();
            }
            randomNumber -= probabilities[i];
            i++;

        }
        
    }

    IEnumerator pause()
    {
        yield return new WaitForSeconds(1.5f);
        timeLeft = time;
        launched = false;
        RandomAction();
    }
    


}

    
 
