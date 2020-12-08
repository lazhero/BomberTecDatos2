using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Players.Behaviors
{
    public class Hide:AiBehavior
    {

        private List<int  > bombPositions;
        private List<int  > BombRatios;
        private List<int[]> solutions;

        public override void Act()
        {
            
            //Primero calcula quien lo mata, luego explora las distintas soluciones, luego encuentra la mas cercana
            bombPositions = myMap.Things["Bomb"];
            BombRatios    = myMap.Things["Ratio"];
            
            int currentPos    = Convert.ToInt32(controller.currentBlock.name);
            List<int> killers = CalculatePossibleKillers(currentPos,bombPositions,BombRatios);
            
            
            if (killers.Count <= 0) return ;
            Backtracking( currentPos);
            Debug.Log("el mejor punto para huir es : "+ ClosestSafePoint() );
        }

        
        private int ClosestSafePoint()
        {
            int closestDist=1000;
            int closestPoint =-1;
            for (int i = 0; i < solutions.Count; i++)
            {
                int[] temporalVariable= solutions[i];
                if (temporalVariable[1] < closestDist)
                {
                    closestDist = temporalVariable[1];
                    closestPoint = temporalVariable[0];
                }
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
        private List<int> CalculatePossibleKillers(int currentPos,List<int> posiciones, List<int> radios) {
            
            List<int>  response= new List<int>();
            
            for (int i = 0; i < posiciones.Count; i++)
            {
                int pos = posiciones[i];
                if (myMap.AreAligned(currentPos,pos))           //estoy enfrente de una bomba
                {
                    int disX = Mathf.Abs(currentPos - pos);
                    int dixY = Mathf.Abs(currentPos - pos) / myMap.WidthAndHeight;
                    
                    if (radios[i]-1 > disX ||radios[i] -1>= dixY) //estoy en su radio de explosion
                        response.Add(pos);
                }
            }

            return response;
        }
        
        
        private void Backtracking( int currentPos)
        {
            solutions = new List<int[]>();
            AuxBacktracking(currentPos,0);

        }


        private void AuxBacktracking(int current,int cost)
        {
            
            if(solutions.Count>4) return;
            int[] possibleSolutions = myMap.TangetPositions(current).ToArray();        //a cuales puedo viajar
            
            foreach (int block in possibleSolutions)                                   //explore cada uno de esos bloques
            {
                if (myMap.canWalkHere(block) && !bombPositions.Contains(block)) { // si no puedo caminar sobre el ignorelo
                    
                    if(CalculatePossibleKillers(block,bombPositions,BombRatios).Count==0)           // me matarian si me pongo aqui?
                        solutions.Add( new []{block,cost});                                //si no me matan  devuelvalo como una opcion
                    
                    else                                                                   //definitivamente si me quedo aqui estare muerto
                        AuxBacktracking(block,cost+1);                          //intento con otro    
                }
            }
        }
    }
}