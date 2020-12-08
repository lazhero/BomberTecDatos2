using System;
using System.Linq;
using DataStructures;
using UnityEngine;

namespace Players.Behaviors
{
    public class Hide:AiBehavior
    {

        private LinkedList<int  > bombPositions;
        private LinkedList<int  > BombRatios;
        private LinkedList<int[]> solutions;
        public override void Act()
        {

            //Primero calcula quien lo mata, luego explora las distintas soluciones, luego encuentra la mas cercana
            bombPositions= myMap.Things["Bombs"];
            BombRatios    = myMap.Things["Ratio"];
            
            int currentPos=Convert.ToInt32(controller.currentBlock.name);
            LinkedList<int> killers = CalculatePossibleKillers(currentPos,bombPositions,BombRatios);
            
            if (killers.Len <= 0) return ;
            Backtracking( currentPos);
            ClosestSafePoint();
            Debug.Log("el mejor punto para huir es : "+ ClosestSafePoint() );
        }

        
        private int ClosestSafePoint()
        {
            int closestPoint=1000;
            for (int i = 0; i < solutions.Len; i++)
            {
                int temporalVariable= solutions[i][1];
                if (temporalVariable < closestPoint)
                    closestPoint = temporalVariable;
            }
            return closestPoint;
        }

        /// <summary>
        /// Estimates whose bombs could kill the player
        /// </summary>
        /// <param name="currentPos"></param>
        /// <param name="posiciones"></param>
        /// <param name="radios"></param>
        /// <returns></returns>
        private LinkedList<int> CalculatePossibleKillers(int currentPos, LinkedList<int> posiciones, LinkedList<int> radios)
        { 
            LinkedList<int>  response= new LinkedList<int>();
            
            for (int i = 0; i < posiciones.Len; i++)
            {
                int pos = posiciones[i];
                if (myMap.AreAligned(currentPos,pos ))//estoy enfrente de una bomba
                    if (radios[i] >= Mathf.Abs(currentPos - pos))//estoy en su radio de explosion
                        response.Add(pos);
            }
            return response;
        }
        
        
        private void Backtracking( int currentPos)
        {
            
            solutions = new LinkedList<int[]>();
            AuxBacktracking(currentPos,0);

        }


        private void AuxBacktracking(int current,int cost)
        {
            
            if(solutions.Len>4) return;
            int[] possibleSolutions = myMap.TangetPositions(current).ToArray();        //a cuales puedo viajar
            
            foreach (int block in possibleSolutions)                                   //explore cada uno de esos bloques
            {
                if (!myMap.canWalkHere(block) || bombPositions.Contains(block)) continue; // si no puedo caminar sobre el ignorelo
                
                if(CalculatePossibleKillers(block,bombPositions,BombRatios).Len==0)           // me matarian si me pongo aqui?
                    solutions.Add( new []{block,cost});                                //si no me matan  devuelvalo como una opcion
                
                else                                                                   //definitivamente si me quedo aqui estare muerto
                    AuxBacktracking(block,cost+1);                          //intento con otro    
               
            }
        }
    }
}