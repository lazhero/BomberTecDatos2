﻿using System;
using UnityEngine;
using AStar;
using System.Collections.Generic;
using SquaredMapTools;
using Things;

public class Map : MonoBehaviour {
    
    private DGraph<GameObject> Graph { get; set; }
    private DGraph<GameObject> otherGraph;
    public int WidthAndHeight { get; set; } = 10;
    public Dictionary<string, List<int>> Things { get; set; }


    [SerializeField] private  GameObject cameraObj;
    [SerializeField] private MapGenerator mg;

    //?--------------------------------->
    //!--------------------------------->
    //?--------------------------------->

    private void Awake()
    {
        Graph = mg.GenerateNewMap();
        otherGraph = mg.otherGraph;
        WidthAndHeight = mg.widthAndHeight;
        cameraObj.transform.position = DeterminesCameraPosition(Graph.Nodes);
        Things= new Dictionary<string, List<int>>();
        Things.Add("item", new List<int>());

    }

    /// <summary>
    /// Returns the route using a*
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public GameObject[] GetRoute(int start, int end)
    {
        if (start < 0 || start >= Graph.Nodes.Length) return null;
        if (end < 0 || end >= Graph.Nodes.Length) return null;
        AStarResponse response= AStar<GameObject>.getRoute(Graph, start, end, WidthAndHeight);
        Stack<int> positions = response.route;
        if (positions.Count <= 1)
        {
            response = AStar<GameObject>.getRoute(Graph, start, response.reacheableClosestNode, WidthAndHeight);
            positions = response.route;
        }
        //positions.Pop();
        GameObject[] squaresArray=new GameObject[positions.Count];
        int i = 0;
        int pos;
       
        while (positions.Count > 0)
        {
            pos = positions.Pop();
            //Debug.Log("La posicion a visitar es "+pos);
            squaresArray[i] = Graph.getNode(pos);
            i++;
        }

        return squaresArray;

    }
    
    /// <summary>
    /// Determines center and height that camera must be
    /// </summary>
    /// <returns></returns>
    private static Vector3 DeterminesCameraPosition( GameObject[] nodes)
    {
        var totalX = 0f;
        var totalZ = 0f;
        foreach (var blokc in nodes)
        {
            var position = blokc.transform.position;
            totalX += position.x;
            totalZ += position.z;
        }

        var len = nodes.Length;
        var centerX = totalX / len;
        var centerZ = totalZ / len;
        var height = Mathf.Sqrt(len) * 1.8f;
        return new Vector3(centerX , height,centerZ*2);
    }
    
        
    /// <summary>
    /// return the 4 related positions
    /// </summary>
    /// <param name="blockName"></param>
    /// <returns></returns>
    public int[] TangetPositions(int blockName)
    {
        return PositionTools.GetRelatedPositions(blockName, WidthAndHeight).ToArray();
    }
    
    /// <summary>
    /// Set relations between two nodes
    /// </summary>
    /// <param name="node"></param>
    /// <param name="price"></param>
    /// <param name="reverse"></param>
    private void SetRelations(int node, int price,bool reverse)
    {
   
        Stack<int> relatedNodes;
        relatedNodes = PositionTools.getRelatedPositions(node, WidthAndHeight);
        while (relatedNodes.Count > 0)
        {
            if(!reverse) Graph. SetRelationShip(node,relatedNodes.Pop(),price);
            else Graph. SetRelationShip(relatedNodes.Pop(),node,price);
        }

    }
    
    
    /// <summary>
    /// reestablish relations when a block is destroyed
    /// </summary>
    /// <param name="node"></param>
    public void BlockDestroyed(int node)
    {
        SetRelations(node,MapGenerator.normalCost,true);
    }

    public void BlockClosed(int node)
    {
        SetRelations(node,Int32.MaxValue ,true);
    }

    /// <summary>
    /// No se de que otra forma ponerlo jaja, esto es para pruebas igualmente.
    /// </summary>
    /// <param name="posAndState"></param>
    /// 
    public void ThingChange(message posAndState)
    {
        
        if (!Things.ContainsKey(posAndState.type))
        {
            Things.Add(posAndState.type,new List<int>());
        }
        
        if(posAndState.action==1) // write
        {
            if(posAndState.value!=null)
            {
                Things[posAndState.type].Add(posAndState.value);
            }
        }

        if (posAndState.action == 2)//overwrite
        {
        }

        if(posAndState.action==3)// erase
        {    
            if(Things[posAndState.type].Contains(posAndState.value))
                Things[posAndState.type].Remove(posAndState.value);             
        }
       
    }
    
    
    /// <summary>
    /// return if two nodes are aligned
    /// </summary>
    /// <param name="pos1"></param>
    /// <param name="pos2"></param>
    /// <returns></returns>
    public bool AreAligned(int pos1,int pos2)
    {
        return PositionTools.areInLine(WidthAndHeight,pos1,pos2);
    }

    public bool canWalkHere(int blockNumber)
    {
        var inf = Graph.getNode(blockNumber).GetComponent<GroundBlock>();
        return inf.blockObject ==null && !inf.isWall ;
    }

    public float calculateDistance(int BlockNumber,string tag,int excludedPos)
    {
        float distance=Int32.MaxValue;
        List<int> myFocusList = Things[tag];
        AStarResponse response;
        int i = -1;
        foreach (var element in myFocusList)
        {
            i++;
            if (i == excludedPos) continue;
            response = AStar<GameObject>.getRoute(otherGraph, BlockNumber, element, WidthAndHeight);
            if (response.value < distance) distance = response.value;
        }

        return distance;

    }
    
}
