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
        cameraObj.transform.position = DeterminesCameraPosition(Graph.Nodes);

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
    
    
}
