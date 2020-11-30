using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using AStar;
using System.Collections.Generic;
using SquaredMapTools;

public class Map : MonoBehaviour {
    [SerializeField] private GameObject indestructibleBlockPrefab;
    [SerializeField] private GameObject[] blocksPrefab;
    [SerializeField] private GameObject groundBlock;
    [SerializeField] private GameObject wallBlock;
    [SerializeField] private GameObject cameraObj;
    [SerializeField] private int widthAndHeight = 10;
    [SerializeField] private float blockSize = 2.1f;
    [SerializeField] private bool debugMode;
    [SerializeField] private bool generateMap = true;
    private DGraph<GameObject> Graph { get; set; }
    private int normalCost = 10;

    
    private int length;
    private int walkableBlocks = 12;
    private int[] forgivenPositions;
    private int[] visitedNodes;
    private  PositionTools op;
        

    void Start() {
        //instancio el grafo y le asigno la cantidad de casillas que va a tener
        //luego inicializo este para que todos empiecen con un valor especifico
        // se determinan las esquinas
        //posteriormente genero el suelo
        //luego genero los demas bloques

        Graph = new DGraph<GameObject>(widthAndHeight * widthAndHeight);
        op= new PositionTools(widthAndHeight);
        forgivenPositions= op. DetermineForgivenPositions(widthAndHeight);
        GenerateGround();
        if(generateMap) GenerateInteractuableBlocks();
        LinkGraph();
        cameraObj.transform.position = op.DeterminesCameraPosition(Graph.Nodes);
        justAPrint();
    }

    void justAPrint()
    {
        //Debug.Log("Voy a entrar a ver las relaciones");
        var relations = Graph.GetRelations(65);
        foreach (var t in relations)
        {
            if (t < Int32.MaxValue)
            {
                //Debug.Log(i);
            }
        }
    }

    void LinkGraph()
    {
        
        for (int i = 0; i < widthAndHeight*widthAndHeight; i++)
        {
            setRelations(i,normalCost,false);
        }
    }

    private void setRelations(int node, int price,bool reverse)
    {
        Stack<int> relatedNodes;
        relatedNodes = PositionTools.getRelatedPositions(node, widthAndHeight);
        while (relatedNodes.Count > 0)
        {
            if(!reverse) setRelation(node,relatedNodes.Pop(),price);
            else setRelation(relatedNodes.Pop(),node,price);
        }

    }

    private void setRelation(int start, int end,float price)
    {
        Graph.SetRelationShip(start,end,price);
    }

   

    /// <summary>
    ///  Instantiate one Block of newObject prefab Specified, with x,y,z coordinates and associate it with the graph
    /// </summary>

    private void GenerateBlock(GameObject newObject, int x, int y, int z)
    {
        // Instancio el objeto y le asigno de padre el mapa
        //establezco a que distance esta
        //le cambio el nombre
        //Uno el nodo con sus nodos circundantes

        var newBlock = Instantiate(newObject, gameObject.transform);
        newBlock.transform.position = new Vector3(x * blockSize, 0, z * blockSize);
        newBlock.gameObject.name = length.ToString();


        Graph.setNodeData(length, newBlock);
        
        // ! DebugThing ERASE LATER --------------------------------------------------->
        if (debugMode)
            newBlock.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = length.ToString();
        else
            newBlock.transform.GetChild(0).gameObject.SetActive(false);

        //! ---------------------------------------------------------------------------->
    }

    /// <summary>
    /// Generates the interactuable blocks by random 
    /// </summary>
    private void GenerateInteractuableBlocks()
    {

        //Lista para el caso que deba borrarlos porque generaron zonas cerradas

        foreach (var node in Graph.Nodes)
        {

            if (op.IsSide(node.name) || op.IsCorner(node.name)) continue;


            GameObject newBlock;
            var groundBlockInfo = node.GetComponent<GroundBlock>();


            if (Random.Range(0, 100) < 70)
            {
                newBlock = Instantiate(blocksPrefab[Random.Range(0, 3)], node.transform, true);
                walkableBlocks++;
            }
            else
            {
                newBlock = Instantiate(indestructibleBlockPrefab, node.transform, true);
                //! debug thing
                newBlock.transform.GetChild(0).gameObject.SetActive(false);
            }


            groundBlockInfo.Reset();

            newBlock.transform.position = node.transform.position + new Vector3(0, 1, 0);
            groundBlockInfo.block = newBlock.GetComponent<Block>();
            groundBlockInfo.blockObject = newBlock;


        }

        /*
        Debug.Log("cantidad de bloques caminables : " + walkableBlocks);
        Debug.Log(message: "cantidad de bloques NOcaminables : " +(int) (Math.Pow(widthAndHeight - 2, 2) - walkableBlocks));
        Debug.Log("tiene areas cerradas : " + !BackTracking());
        */
        if (BackTracking()) return;
        walkableBlocks = 12;
        Invoke(nameof(GenerateInteractuableBlocks), 0.1f);


    }

    /// <summary>
    /// Generates the indestructible world ground with walls for each "x" line generates a block for each "z" value
    /// </summary>
    private void GenerateGround()
    {
        for (var z = 0; z < widthAndHeight; z++)
        {
            for (var x = 0; x < widthAndHeight; x++)
            {
                if (x == 0 || x == widthAndHeight - 1 || z == 0 || z == widthAndHeight - 1)
                    GenerateBlock(wallBlock, -x, 0, z);

                else
                    GenerateBlock(groundBlock, -x, 0, z);

                length++;

            }
        }
    }

    /// <summary>
    /// Explores all the map looking for closed areas, if they are, returns false, compare if the visited nodes match with walkable blocks number
    /// </summary>
    /// <returns>bool</returns>
    private bool BackTracking() {
        
        visitedNodes = new int[walkableBlocks]; //espero visitar todos los nodos caminables
        var contador = BackTrackingAux(forgivenPositions[0], 0);
        return contador == walkableBlocks;
    }

    /// <summary>
    /// Explores all nodes recursively , ask if it is a walkable one, if true, the counter increases
    /// </summary>
    /// <param name="blockNumber"></param>
    /// <param name="cont"></param>
    /// <returns> int how many nodes it visited</returns>
    private int BackTrackingAux(int blockNumber, int cont) {

        if (op.IsSide(blockNumber)) return cont;
        var blockInfo= Graph.getNode(blockNumber).GetComponent<GroundBlock>();

        if (visitedNodes.Contains(blockNumber)) return cont;
        if (blockInfo.block != null) 
            if (!blockInfo.block.isDestructible ) return cont;
        
        visitedNodes[cont] = blockNumber;
        cont++;

        var blockUp    = op.DetectWalkable(blockNumber, op.Up);
        var blockDown  = op.DetectWalkable(blockNumber, op.Down);
        var blockLeft  = op.DetectWalkable(blockNumber, op.Left);
        var blockRight = op.DetectWalkable(blockNumber, op.Right);
            
        if (blockUp    > 0)    cont = BackTrackingAux(blockUp   , cont);
        if (blockDown  > 0)    cont = BackTrackingAux(blockDown , cont);
        if (blockLeft  > 0)    cont = BackTrackingAux(blockLeft , cont);
        if (blockRight > 0)    cont = BackTrackingAux(blockRight, cont);




        return cont;

    }
    
    public GameObject[] GetRoute(int start, int end)
    {
      //  Debug.Log("Pos he llegado");
       // Debug.Log("El row number es "+widthAndHeight);
       AStarResponse response= AStar.AStar<GameObject>.getRoute(Graph, start, end, widthAndHeight);
       Stack<int> positions = response.route;
      // Debug.Log("El len del stack es "+positions.Count);
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

    public void BlockDestroyed(int node)
    {
        setRelations(node,normalCost,true);
    }
    

}
