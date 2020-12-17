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
    private int FreeCost = 10;
    private int BlockedCost=100;
    private int ClosedCost=Int32.MaxValue;
    
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
        InvokeRepeating("changeMyProbabilities",5,Time.deltaTime*rateTime);
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
        float score;
        float SucessScore;
        float ShortestScore;
        for (int i = 0; i < sucessRate.Length; i++)
        {
            if (Being[i] == null) sucessRate[i] = 0;
            else
            {
                score = scores[i].score;
                SucessScore = scores[i].SuccessBombs * 1000;
                ShortestScore = getCostFromCloserBomb(scores[i].shortestDistanceFromPlayer);
                sucessRate[i] = score+SucessScore+ShortestScore;
                if (sucessRate[i] > 10000) ;
            }
            
            
        }

        return sucessRate;
    }

    private float getCostFromCloserBomb(float price)
    {
        if (price >= ClosedCost) return 0;
        int FreeBlocks;
        int BlockedBlocks;
        int value=0;
        BlockedBlocks = (int)price / BlockedCost;
        price -= BlockedBlocks * BlockedCost;
        FreeBlocks = (int) price / FreeCost;
        if (FreeBlocks <= 10)
        {
            value += 500 - 10 * FreeBlocks;
        }

        if (BlockedCost <=5)
        {
            value+=500-100*BlockedCost;
        }
        return value;
        
    }

    void changeMyProbabilities()
    {
        float[][] currentPopulationGenes = getActualPoblation();
        float[] currentScores = getSucessOfActualPoblation();
        float minScore = getAverage(currentScores);
        float[][] newPopulation = ProbabilitiesGenetic.Genetic(currentPopulationGenes, currentScores, minScore);
        resetScores();
        setStats(newPopulation);

    }

    void resetScores()
    {
        foreach (var score in scores)
        {
            if(score!=null) score.resetToClean();
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

    public void updateClosestBomb(int pos, float value)
    {
        int index = pos - startPos;
        if (!verification(index)) return;
        ScoreTable currentFocus=scores[index];
       if(currentFocus.shortestDistanceFromPlayer>value) currentFocus.shortestDistanceFromPlayer = value;
    }

    public void AddSucessBomb(int pos)
    {
        int index = pos - startPos;
        if (!verification(index)) return;
        scores[index].SuccessBombs++;
    }

    private bool verification(int pos)
    {
        return pos >= 0 && pos < Being.Length;
    }
}
