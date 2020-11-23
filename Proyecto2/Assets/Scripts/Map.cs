using System;
using System.Linq;
using DataStructures;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject indestructibleBlockPrefab;
    [SerializeField] private GameObject[] blocksPrefab;
    [SerializeField] private GameObject groundBlock;
    [SerializeField] private GameObject wallBlock;
    [SerializeField] private int widthAndHeight = 10;
    [SerializeField] private float blockSize = 2.1f;
    [SerializeField] private bool debugMode;
    [SerializeField] private int normalCost = 10;

    private DGraph<GameObject> Graph { get; set; }

    private static int[] UP = {0, -1};
    private static int[] DOWN = {0, 1};
    private static int[] RIGHT = {1, 0};
    private static int[] LEFT = {-1, 0};

    private int length;
    private int walkableBlocks = 12;
    private int[] forgivenPositions;
    private int[] visitedNodes;


    void Start()
    {
        //instancio el grafo y le asigno la cantidad de casillas que va a tener
        //luego inicializo este para que todos empiecen con un valor especifico
        // se determinan las esquinas
        //posteriormente genero el suelo
        //luego genero los demas bloques

        Graph = new DGraph<GameObject>(widthAndHeight * widthAndHeight);
        Graph.startAllWith(10);
        DetermineForgivenPositions();
        GenerateGround();
        GenerateInteractuableBlocks();

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
        LinkWithBlock(UP, 0);
        LinkWithBlock(DOWN, 1);
        LinkWithBlock(LEFT, 2);
        LinkWithBlock(RIGHT, 3);


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

            if (IsSide(node.name) || IsCorner(node.name)) continue;


            GameObject newBlock;
            var groundBlockInfo = node.GetComponent<GroundBlock>();


            if (Random.Range(0, 100) < 65)
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

       Debug.Log("cantidad de bloques caminables : " + walkableBlocks);
        Debug.Log(message: "cantidad de bloques NOcaminables : " +(int) (Math.Pow(widthAndHeight - 2, 2) - walkableBlocks));
        Debug.Log("tiene areas cerradas : " + !BackTracking());

        if(BackTracking()) return;
        Debug.Log("generando mapa otra vez");
        walkableBlocks = 12;
        Invoke(nameof(GenerateInteractuableBlocks),3);


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
    /// Associate block with the GRAPH and associate it with is neighbors
    /// </summary>
    private void LinkWithBlock(int[] dir, int pos)
    {

        if (WhoIs(dir) != -1 && dir != null)

            Graph.SetRelationShip(length, pos, WhoIs(dir));
    }


    /// <summary>
    /// this calculates who is up, down , left, or right depending on a Vector2 (x,y) return -1 if the direction is not possible
    /// (1,0) is right (0,1) is up
    /// </summary>
    private int WhoIs(int[] direction)
    {
        return WhoIs(length, direction);
    }

    /// <summary>
    /// this calculates who is up, down , left, or right depending on a Vector2 (x,y) return -1 if the direction is not possible
    /// (1,0) is right (0,1) is up
    /// </summary>
    private int WhoIs(int BlockNumber, int[] direction)
    {

        if (!ValidateDirection(direction, BlockNumber)) return -1;

        return Convert.ToInt32(BlockNumber + direction[0] + direction[1] * widthAndHeight);

    }

    private int DettectWalkable(int BlockNumber, int[] direction) {
        var response=WhoIs(BlockNumber,direction);
        
        if (IsSide(response)) return -1;


        return response;

    }

    /// <summary>
    /// validates certain direction from given block number, THIS IS ONLY FOR MAP GENERATION
    /// you dont need to know how it works
    /// </summary>
    private bool ValidateDirection(int[] direction, int blockNumber) {

        //si estoy en un borde izquierdo y me preguntan por su izquierdo
        //si estoy en un borde derecho y me preguntan por su derecho        
        //si estoy en el borde superior y me preguntan por el de arriba
        //si estoy en el borde inferior y me preguntan por el de abajo

        if (blockNumber % widthAndHeight == 0 && direction[0] < 0) return false;

        if ((blockNumber + 1) % widthAndHeight == 0 && direction[0] > 0) return false;

        if (blockNumber - widthAndHeight < 0 && direction[1] < 0) return false;

        if (blockNumber + widthAndHeight > widthAndHeight * widthAndHeight && direction[1] > 0) return false;

        return true;

    }


    
    private bool BackTracking() {
        visitedNodes = new int[walkableBlocks];  //espero visitar todos los nodos caminables
        var contador = BackTrackingAux(forgivenPositions[0], 0);
        Debug.Log("Casillas exploradas : "+contador);
        return  contador== walkableBlocks ;
    }
    
    private int BackTrackingAux(int BlockNumber, int cont)
    {

        if (IsSide(BlockNumber)) return cont;
        var blockInfo= Graph.getNode(BlockNumber).GetComponent<GroundBlock>();

        if (visitedNodes.Contains(BlockNumber)) return cont;
        if (blockInfo.block != null) 
            if (!blockInfo.block.isDestructible ) return cont;
        
        visitedNodes[cont] = BlockNumber;
        cont++;

        var BlockUp    = DettectWalkable(BlockNumber, UP);
        var BlockDown  = DettectWalkable(BlockNumber, DOWN);
        var BlockLeft  = DettectWalkable(BlockNumber, LEFT);
        var BlockRight = DettectWalkable(BlockNumber, RIGHT);
            
        if (BlockUp    > 0)    cont = BackTrackingAux(BlockUp   , cont);
        if (BlockDown  > 0)    cont = BackTrackingAux(BlockDown , cont);
        if (BlockLeft  > 0)    cont = BackTrackingAux(BlockLeft , cont);
        if (BlockRight > 0)    cont = BackTrackingAux(BlockRight, cont);




        return cont;

    }
    
    

    /// <summary>
    /// Determines if the given block is a side of the map, it also verifies that the given parameter is a int
    /// </summary>
    /// <param name="blockNumberAux"></param>
    /// <returns> bool</returns>
    private bool IsSide(object blockNumberAux) {
        int blockNumber;

        if (blockNumberAux is string)
            blockNumber = Convert.ToInt32(blockNumberAux);
        else
            blockNumber = (int) blockNumberAux;

        return blockNumber % widthAndHeight == 0 || (blockNumber + 1) % widthAndHeight == 0 ||
               blockNumber - widthAndHeight < 0 || blockNumber + widthAndHeight > widthAndHeight * widthAndHeight;
    }

    /// <summary>
    ///  Returns if a specified block is a corner can accept int or string 
    /// </summary>
    /// <param name="blockNumberAux"></param>
    /// <returns></returns>
    private bool IsCorner(object blockNumberAux){
        int blockNumber;

        if (blockNumberAux is string)
            blockNumber = Convert.ToInt32(blockNumberAux);
        else
            blockNumber = (int) blockNumberAux;
        return forgivenPositions.Contains(blockNumber);
    }
    
    /// <summary>
    /// Determines the forgiven Positions for n*n matrix
    /// </summary>
    private void DetermineForgivenPositions() {
        var n = widthAndHeight;
        var n2 = n * n;
        forgivenPositions = new int[12];
        forgivenPositions[0] = n + 1;
        forgivenPositions[1] = n + 2;
        forgivenPositions[2] = 2*n + 1;
        
        forgivenPositions[3] = 2*n  -2;
        forgivenPositions[4] = 2*n  -3;
        forgivenPositions[5] = 3*n +-2;
        
        forgivenPositions[6] = n2-2*n + 1;
        forgivenPositions[7] = n2-2*n + 2;
        forgivenPositions[8] = n2-3*n + 1;
        
        forgivenPositions[9] = n2- n -2 ;
        forgivenPositions[10] = n2-2*n -2 ;
        forgivenPositions[11] = n2-n -   3;
    }

}
