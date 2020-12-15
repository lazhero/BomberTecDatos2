using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Genetics;
using UnityEngine;

public class Mendel : MonoBehaviour
{
    public GameObject[] Being { set; get; }
    public float rateTime;
    private IAProbability[] ProbabilitiesComponents;
    public ScoreTable[] scores;
    
    public int startPos { get; set; }
    public float min;
    
    

    public void AddNewBean(GameObject player)
    {
        for (int iterator = 0; iterator < Being.Length; iterator++)
        {
            if (Being[iterator] == null)
            {
                Being[iterator] = player;
                break;
            }
        }
    }
    




    public void init()
    {
        GetProbabiliesComponent();
        GetScoresTableComponent();
        float[][] initialProbabilities=ProbabilitiesGenetic.generateInitialPopulation(Being.Length, ProbabilitiesComponents[0].getNumberOfActions());
        setStats(initialProbabilities);
        InvokeRepeating("changeMyProbabilities",1,Time.deltaTime*rateTime);
    }
    
    

    void GetProbabiliesComponent()
    {
        ProbabilitiesComponents=new IAProbability[Being.Length];
        for (int i = 0; i < Being.Length; i++)
        {
            ProbabilitiesComponents[i] = Being[i].GetComponent<IAProbability>();
        }
    }

    void GetScoresTableComponent()
    {
        scores=new ScoreTable[Being.Length];
        for (int i = 0; i < Being.Length; i++)
        {
            scores[i] = Being[i].GetComponent<ScoreTable>();
        }
    }

    void setStats(float[][] stats)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            ProbabilitiesComponents[i].probabilities = stats[i];
            scores[i].score = 0;
        }
    }

    float[][] getActualPoblation()
    {
        float[][] population=new float[Being.Length][];
        for (int i = 0; i < population.Length; i++)
        {
            population[i] = ProbabilitiesComponents[i].probabilities;
        }

        return population;
    }

    float[] getSucessOfActualPoblation()
    {
        float[] sucessRate = new float[Being.Length];
        for (int i = 0; i < sucessRate.Length; i++)
        {
            sucessRate[i] = scores[i].score;
        }

        return sucessRate;
    }

    void changeMyProbabilities()
    {
        float[][] currentPopulationGenes = getActualPoblation();
        float[] currentScores = getSucessOfActualPoblation();
        float minScore = getAverage(currentScores);
        float[][] newPopulation = ProbabilitiesGenetic.Genetic(currentPopulationGenes, currentScores, minScore);
        scoresToValue(0);
        setStats(newPopulation);

    }

    void scoresToValue(float value)
    {
        foreach (var score in scores)
        {
            score.score = value;
        }
    }

    public void updateValue(int position,int value)
    {
        int index = position - startPos;
        if (index < 0 || index >= scores.Length) return;
        scores[index].score += value;
    }

    float getAverage(float[] array)
    {
        int i = 0;
        float sum = 0;
        foreach (float current in array)
        {
            sum += current;
            i++;
        }
        if (i == 0) return 0;
        return sum / i;
    }
}
