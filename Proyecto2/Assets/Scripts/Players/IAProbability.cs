using DataStructures;
using Players.Behaviors;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class IAProbability : MonoBehaviour
{
    [SerializeField] private float[] probabilities;
    [SerializeField] private AiBehavior[] behaviors;
    [SerializeField] private int time;

    private IAMovementController movement;

    private void Start()
    {
        movement = gameObject.GetComponent<IAMovementController>();
        behaviors = GetComponents<AiBehavior>();
        //InvokeRepeating("RandomWithoutProbs", 1, time * Time.deltaTime);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
            GetComponent<Follow>().Act();
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
    /// <summary>
    /// this is only for test
    /// </summary>
    public void RandomWithoutProbs()
    {
        behaviors[Random.Range(0, behaviors.Length)].Act();
    }
    /// <summary>
    /// this is only for test
    /// </summary>
    public void RandomAction2()
    {
        float randomNumber = Random.Range(0, 100);
        int i = 1;
        float sumatoria = 0;
        while (randomNumber > 0)
        {
            sumatoria += probabilities[i];
            if (randomNumber < sumatoria)
            {
                behaviors[i].Act();
            }
            i++;
        }
        
    }
}

    
 
