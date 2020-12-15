
using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataStructures;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace Genetics
{
    public class ProbabilitiesGenetic
    {
        private static float successRate;
        private static int maxValue = 100;
        public static float[][] Genetic(float[][] populationGenomes, float[] successRate, float minSuccess)
        {
            ProbabilitiesGenetic.successRate = minSuccess;
            float[][] sucessPopulation = GetSuccess(populationGenomes, successRate);
            float[][] newPopulation = GetNewPopulation(populationGenomes.Length, sucessPopulation);
            return newPopulation;
        }

        public static float[][] GetSuccess(float[][] population, float[] success)
        {
            
            Stack<float[]> sucessFullPopulation =new Stack<float[]>();
            for (int i = 0; i < success.Length; i++)
            {
                if (Finess(success[i]) && population[i]!=null)
                {
                    sucessFullPopulation.Push(population[i]);
                }
            }

            return ArrayTools<float[]>.getArrayFromStack(sucessFullPopulation);
        }
        

        public static bool Finess(float success)
        {
            return success >= successRate;
        }

        public static float[] Reproduction(float[] gene1, float[] gene2)
        {
            float[][] parentsArray=new float[2][];
            parentsArray[0] = gene1;
            parentsArray[1] = gene2;
            float[] son=new float[gene1.Length];
            int parentPos;
            for (int i = 0; i < son.Length; i++)
            {
                parentPos = Random.Range(0, parentsArray.Length) % parentsArray.Length;// un poco de hardcoding , pero esto no va cambiar nunca, una correcion posible seria meter ambos genes en un array 
                son[i] = parentsArray[parentPos][i];

            }
            return son;
        }

        public static void FixGenome(float[] genome)
        {
            float difference = maxValue - GetSum(genome);
            if (Math.Abs(difference) < 0.1) return;
            float change = (difference) / genome.Length;
            for (int i = 0; i < genome.Length; i++)
            {
                genome[i] += change;
                if (genome[i] < 0) genome[i] = 0;
                if (genome[i] > maxValue) genome[i] = maxValue;
            }
           // FixGenome(genome);
        }

        public static float GetSum(float[] genome)
        {
            float sum=0;
            foreach (var gen in genome)
            {
                sum += gen;
            }

            return sum;
        }

        public static void Mutate(float[][] population)
        {
            int randomPositionA;
            int randomPositionB;
         
            foreach (var genome in population)
            {
                randomPositionA = Random.Range(0, genome.Length + 1)%genome.Length;
                randomPositionB = Random.Range(0, genome.Length + 1)%genome.Length;
                if (genome[randomPositionA] < maxValue && genome[randomPositionB] > 0)
                {
                    genome[randomPositionA]++;
                    genome[randomPositionB]--;
                }
                FixGenome(genome);
                
            }
        }

        public static float[][] GetNewPopulation(int membersNumber,float[][] successPopulation)
        {
            float[][] newPopulation=new float[membersNumber][];
            float[] current;
            int randomPosA;
            int randomPosB;
            for (int i = 0; i < membersNumber; i++)
            {
                randomPosA = Random.Range(0, successPopulation.Length)%successPopulation.Length;
                randomPosB=Random.Range(0, successPopulation.Length)%successPopulation.Length;
                if(randomPosA>=successPopulation.Length || randomPosB>=successPopulation.Length)Debug.Log("Me pase de las dimensiones");
                
                current = Reproduction(successPopulation[randomPosA], successPopulation[randomPosB]);
                
                newPopulation[i] = current;
            }

            Mutate(newPopulation);
            return newPopulation;
        }

        public static float[][] generateInitialPopulation(int Number,int attributesNumber)
        {
            float[][] population = new float[Number][];
            for (int i = 0; i < Number; i++)
            {
                population[i] = generateBeing(attributesNumber);
            }

            return population;
        }

        public static float[] generateBeing(int attributesNumber)
        {
            float left = maxValue;
            float selected;
            int randomPos;
            float[] genome = new float[attributesNumber];
            while (left > 0.01)
            {
                
                randomPos = (int)(Random.Range(0, attributesNumber));
                randomPos = randomPos % attributesNumber;
                selected = Random.Range(0, left);
                genome[randomPos] +=(float) Math.Round(selected,2);
                left -= selected;
            }
            return genome;

        }
        
    }
}