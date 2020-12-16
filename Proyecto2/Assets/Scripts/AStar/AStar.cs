﻿﻿using System;
using System.Collections;
using System.Collections.Generic;
using DataStructures;
  using UnityEngine;


  namespace AStar
{
    public class AStar<T>
    {
        private static int nullValue = -1;
        private static float[][] pricesMatrix;   //managing the relation matrix of the graph
        
        
        
        private static float[] PricesFromTheNode;  //The price from the focus node to anotherNode  // this will save the G value
        private static int[] heuristic; //this will save the h
        private static int[] predecessor;  //saving the predecessor for each node
        private static bool[] visited;
        private static int nodewithTheLowestHeuristic;

        /// <summary>
        /// Get the route from one to another using the A* algorithms, this is though as a chest board for calculating the heuristic through the manhathan distance
        /// </summary>
        /// <param name="graph"> The graph give </param>
        /// <param name="startPosition"> the initial node position</param>
        /// <param name="endPosition"> the final node position</param>
        /// <param name="rowSize"> The size of each row </param>
        /// <returns>A holder with the route and the price to the final node </returns>
        public static AStarResponse getRoute(DGraph<T> graph,int startPosition,int endPosition,int rowSize)
        {
            pricesMatrix = graph.GetAllRelations();
            PricesFromTheNode=new float[pricesMatrix.Length];
            predecessor = new int[pricesMatrix.Length];
            heuristic = new int[pricesMatrix.Length];
            visited=new bool[pricesMatrix.Length];
            
            ArrayTools<int>.initArray(predecessor,nullValue);
            ArrayTools<float>.initArray(PricesFromTheNode,Int32.MaxValue);
            ArrayTools<bool>.initArray(visited,false);
            
            calculateHeuristic(endPosition,rowSize);
            nodewithTheLowestHeuristic = startPosition;
            PricesFromTheNode[startPosition] = 0;
            processSteps(startPosition,endPosition);
            
            return buildAStar(startPosition, endPosition);


        }
        /// <summary>
        /// Makes the process of traversing the graph, until have found the end node or a situacion where theres node available node
        /// </summary>
        /// <param name="start">start position </param>
        /// <param name="endPos">End position</param>
        private static void processSteps(int start,int endPos)
        {
            int focusPos = start;
            while (focusPos != endPos && focusPos>=0)
            {
                addNodeClosed(focusPos);
                focusPos = findTheLowest();
            }
            
        }
        /// <summary>
        /// Finds a node with the lower F, and that is not of the visited nodes
        /// </summary>
        /// <returns>The position of the node found</returns>
        private static int findTheLowest()
        {
            float min = Int32.MaxValue;
            int position=nullValue;
            for (int i = 0; i < PricesFromTheNode.Length; i++)
            {
                if (PricesFromTheNode[i]!=Int32.MaxValue && PricesFromTheNode[i]+heuristic[i] < min && !visited[i])
                {
                    position = i;
                    min = PricesFromTheNode[i]+heuristic[i];
                }
            }

            return position;
        }
        /// <summary>
        /// Calculate the heuristic for each node, working with a squared board 
        /// </summary>
        /// <param name="final"> goal position node</param>
        /// <param name="rowSize">the size of each node</param>
        private static void calculateHeuristic(int final,int rowSize)
        {
            int focusRow;
            int focusColumn;
            int finalRow = calculateRow(final, rowSize);
            int finalColumn = calculateColumn(final, rowSize);
            for (int i = 0; i < heuristic.Length; i++)
            {
                focusRow = calculateRow(i, rowSize);
                focusColumn = calculateColumn(i, rowSize);
                heuristic[i] = Math.Abs(focusRow - finalRow) + Math.Abs(focusColumn - finalColumn);

            }
            
        }
        /// <summary>
        /// Add a node to the closed nodes, and adds the the nodes connected to it to the open nodes if they're not there already
        /// </summary>
        /// <param name="position"> Position of the node to add </param>
        private static void addNodeClosed(int position)
        {
            
            visited[position] = true;
            float actualPrice;
            float newPrice;
            float[] relations = pricesMatrix[position];
            for (int i = 0; i < relations.Length; i++)
            {
                actualPrice = PricesFromTheNode[i];
                newPrice = PricesFromTheNode[position] + relations[i];
                if (newPrice<actualPrice)
                {
                    PricesFromTheNode[i] = newPrice;
                    predecessor[i] = position;
                }
            }

            if (heuristic[position] < heuristic[nodewithTheLowestHeuristic]) nodewithTheLowestHeuristic = position;

        }

        /// <summary>
        /// Calculate the row of a node
        /// </summary>
        /// <param name="data"> Number of the node</param>
        /// <param name="rowSize">The size of the row</param>
        /// <returns>the row number</returns>
        private static int calculateRow(int data, int rowSize)
        {
            return data / rowSize;
        }
        /// <summary>
        ///Calculate the column of a node
        /// </summary>
        /// <param name="data"> the number of the node</param>
        /// <param name="rowSize">the size of the row</param>
        /// <returns>the column number</returns>
        /// 
        private static int calculateColumn(int data, int rowSize)
        {
            return data % rowSize;
        }
        /// <summary>
        /// Builds the package response 
        /// </summary>
        /// <param name="starPos">start position</param>
        /// <param name="endPos">end position</param>
        /// <returns>An AStarResponse packing the route and the price </returns>
        private static AStarResponse buildAStar(int starPos,int endPos)
        {
            int pos = endPos;
            Stack<int> route=new Stack<int>();
            while (pos != nullValue)
            {
                route.Push(pos);
                pos = predecessor[pos];
            }
            AStarResponse response=new AStarResponse();
            response.route = route;
            response.value = PricesFromTheNode[endPos];
            response.reacheableClosestNode = nodewithTheLowestHeuristic;
            
            return response;

        }
        
    }
}
