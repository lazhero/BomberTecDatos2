using System;
using UnityEngine;
using AStar;
using System.Collections.Generic;
using SquaredMapTools;

public class Map : MonoBehaviour {
    
    private DGraph<GameObject> Graph { get; set; }
    private int widthAndHeight = 10;
    [SerializeField] private  GameObject cameraObj;
    [SerializeField] private MapGenerator mg;


    private void Start()
    {
        Graph = mg.GenerateNewMap();
        
        widthAndHeight = mg.widthAndHeight;
        cameraObj.transform.position = PositionTools.DeterminesCameraPosition(Graph.Nodes);

    }


    public GameObject[] GetRoute(int start, int end)
    {

        AStarResponse response= AStar<GameObject>.getRoute(Graph, start, end, widthAndHeight);
        Stack<int> positions = response.route;
        positions.Pop();
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
}
