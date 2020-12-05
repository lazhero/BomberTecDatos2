
using System;
using System.Collections.Generic;
using DataStructures;
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
                if (Finess(success[i]))
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
            float[] son=new float[gene1.Length];
            for (int i = 0; i < gene1.Length / 2;i++)
            {
                son[i] = gene1[i];
            }

            for (int i = gene1.Length / 2; i < gene1.Length; i++)
            {
                son[i] = gene2[i];
            }
            FixGenome(son);
            return son;
        }

        public static void FixGenome(float[] genome)
        {
            float difference = maxValue - GetSum(genome);
            float change = (-difference) / genome.Length;
            for (int i = 0; i < genome.Length; i++)
            {
                genome[i] += change;
            }
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
                randomPositionA = Random.Range(0, genome.Length + 1);
                randomPositionB = Random.Range(0, genome.Length + 1);
                if (genome[randomPositionA] < maxValue && genome[randomPositionB] > 0)
                {
                    genome[randomPositionA]++;
                    genome[randomPositionB]--;
                }
                
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
                randomPosA = Random.Range(0, successPopulation.Length + 1);
                randomPosB=Random.Range(0, successPopulation.Length + 1);
                current = Reproduction(successPopulation[randomPosA], successPopulation[randomPosB]);
                newPopulation[i] = current;
            }

            Mutate(newPopulation);
            return newPopulation;
        }
        
    }
}