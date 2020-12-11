﻿
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

            for (int i = gene2.Length / 2; i < gene1.Length; i++)
            {
                son[i] = gene2[i];
            }
            FixGenome(son);
            return son;
        }

        public static void FixGenome(float[] genome)
        {
            float difference = maxValue - GetSum(genome);
            if (Math.Abs(difference) < 0.01) return;
            float change = (-difference) / genome.Length;
            for (int i = 0; i < genome.Length; i++)
            {
                genome[i] += change;
                if (genome[i] < 0) genome[i] = 0;
                if (genome[i] > maxValue) genome[i] = maxValue;
            }
            FixGenome(genome);
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
                randomPosA = Random.Range(0, successPopulation.Length + 1)%successPopulation.Length;
                randomPosB=Random.Range(0, successPopulation.Length + 1)%successPopulation.Length;
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