using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using AStar;
using System.Collections.Generic;

namespace SquaredMapTools
{
    public class MapGenerator: MonoBehaviour
    {
    [SerializeField] private   GameObject indestructibleBlockPrefab;
    [SerializeField] private   GameObject[] blocksPrefab;
    [SerializeField] private   GameObject groundBlock;
    [SerializeField] private   GameObject wallBlock;
    [SerializeField] public int widthAndHeight = 10;
    [SerializeField] private bool debugMode;
    [SerializeField] private bool generateMap = true;
    public DGraph<GameObject> Graph { get; set; }
    private int normalCost = 10;
    private int blockedCost = Int32.MaxValue;
    private int closedCost = Int32.MaxValue;
    private int length;
    private int walkableBlocks = 28;
    private int[] forgivenPositions;
    private int[] visitedNodes;
    private  float blockSize = 1.9f;

        

    public DGraph<GameObject> GenerateNewMap()
    {
        Graph = new DGraph<GameObject>(widthAndHeight * widthAndHeight);
        forgivenPositions= PositionTools. DetermineForgivenPositions(widthAndHeight);
        GenerateGround();
        if(generateMap) GenerateInteractuableBlocks();

        return Graph;
    }
    void justAPrint()
    {
        Debug.Log("Voy a entrar a ver las relaciones");
        var relations = Graph.GetRelations(27);
       
    }

    void LinkGraph()
    {
       // GameObject[] nodes = Graph.Nodes;
       // GameObject Node;
        for (int i = 0; i < widthAndHeight*widthAndHeight; i++)
        {
            setRelations(i,normalCost,true);
            
        }
    }

    private void setRelations(int node, int price,bool reverse)
    {
   
        Stack<int> relatedNodes;
        relatedNodes = SquaredMapTools.PositionTools.getRelatedPositions(node, widthAndHeight);
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

    private void GenerateBlock(GameObject newObject, int x, float y, int z)
    {
        // Instancio el objeto y le asigno de padre el mapa
        //establezco a que distance esta
        //le cambio el nombre
        //Uno el nodo con sus nodos circundantes

        var newBlock = Instantiate(newObject, gameObject.transform);
        newBlock.transform.position = new Vector3(x * blockSize, y*blockSize, z * blockSize);
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
        int i;
        foreach (var node in Graph.Nodes)
        {
            i=Int32.Parse(node.name);
            
            //Debug.Log("Voy por el numero del "+node.name);
            
            if (PositionTools.IsSide(node.name,widthAndHeight)) continue;
            if (PositionTools.IsAForgivenOne(node.name, forgivenPositions))
            {
                setRelations(i,normalCost,true);
                continue;
            }
            GameObject newBlock;
            var groundBlockInfo = node.GetComponent<GroundBlock>();


            if (Random.Range(0, 100) < 70)
            {
                setRelations(i,blockedCost,true);
                newBlock = Instantiate(blocksPrefab[Random.Range(0, 3)], node.transform, true);
                walkableBlocks++;
            }
            else
            {
                setRelations(i, closedCost, true);
                newBlock = Instantiate(indestructibleBlockPrefab, node.transform, true);
                newBlock.transform.GetChild(0).gameObject.SetActive(false);
            }
            

            groundBlockInfo.Reset();

            newBlock.transform.position = node.transform.position + new Vector3(0, 2, 0);
            groundBlockInfo.block = newBlock.GetComponent<Block>();
            groundBlockInfo.blockObject = newBlock;

        }
        Debug.Log("cantidad de bloques caminables : " + walkableBlocks);
        Debug.Log(message: "cantidad de bloques NOcaminables : " +(int) (Math.Pow(widthAndHeight - 2, 2) - walkableBlocks));
        Debug.Log("tiene areas cerradas : " + !BackTracking());
        if(BackTracking()) return;
        walkableBlocks = 28;
        Invoke(nameof(GenerateInteractuableBlocks),0.1F);
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
                    GenerateBlock(wallBlock, -x, 0.8f, z);

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

        if (PositionTools.IsSide(blockNumber,widthAndHeight)) return cont;
        var blockInfo= Graph.getNode(blockNumber).GetComponent<GroundBlock>();

        if (visitedNodes.Contains(blockNumber)) return cont;
        if (blockInfo.block != null) 
            if (!blockInfo.block.isDestructible ) return cont;
        
        visitedNodes[cont] = blockNumber;
        cont++;

        var blockUp    = PositionTools.DetectWalkable(blockNumber, PositionTools.Up,widthAndHeight);
        var blockDown  = PositionTools.DetectWalkable(blockNumber, PositionTools.Down,widthAndHeight);
        var blockLeft  = PositionTools.DetectWalkable(blockNumber, PositionTools.Left,widthAndHeight);
        var blockRight = PositionTools.DetectWalkable(blockNumber, PositionTools.Right,widthAndHeight);
            
        if (blockUp    > 0)    cont = BackTrackingAux(blockUp   , cont);
        if (blockDown  > 0)    cont = BackTrackingAux(blockDown , cont);
        if (blockLeft  > 0)    cont = BackTrackingAux(blockLeft , cont);
        if (blockRight > 0)    cont = BackTrackingAux(blockRight, cont);




        return cont;

    }
    


    public void BlockDestroyed(int node)
    {
        setRelations(node,normalCost,true);
    }
    
    }
}