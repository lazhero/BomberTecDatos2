using System;
using System.Linq;
using DataStructures;
using UnityEngine;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject groundBlock;
    [SerializeField] private GameObject wallBlock;
    [SerializeField] private int widthAndHeight = 10;
    [SerializeField] private int length;
    [SerializeField] private float blockSize = 2.1f;
    [SerializeField] private bool debugMode;
    [SerializeField] private int normalCost = 10;
    [SerializeField] private GameObject[] blocks;
    [SerializeField] private GameObject indestructibleBlock;
    private int[] forgivenPositions;
    private DGraph<GameObject> Graph { get; set; }
    private static int[] UP = {0, -1};
    private static int[] DOWN = {0, 1};
    private static int[] RIGHT = {1, 0};
    private static int[] LEFT = {-1, 0};


    
    void Start() {
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

    private void GenerateBlock(GameObject newObject, int x, int y, int z) {
        // Instancio el objeto y le asigno de padre el mapa
        //establezco a que distance esta
        //le cambio el nombre
        //Uno el nodo con sus nodos circundantes

        var newBlock = Instantiate(newObject, gameObject.transform);
        newBlock.transform.position = new Vector3(x * blockSize, 0, z * blockSize);
        newBlock.gameObject.name = length.ToString();


        Graph.setNodeData(length, newBlock);
        linkWithBlock(UP, 0);
        linkWithBlock(DOWN, 1);
        linkWithBlock(LEFT, 2);
        linkWithBlock(RIGHT, 3);


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
    private void GenerateInteractuableBlocks() {

        //Lista para el caso que deba borrarlos porque generaron zonas cerradas
        var referenceList = new LinkedList<GameObject>();

        foreach (var node in Graph.Nodes)
        {

            if (IsSide(node.name) || IsCorner(node.name)) continue;

            GameObject newBlock;
            var blokInfo = node.GetComponent<GroundBlock>();
            
            
            if (Random.Range(0, 100) < 80)
            {
                newBlock = Instantiate(blocks[Random.Range(0, 3)], node.transform, true);
            }
            else 
            {    
                newBlock = Instantiate(indestructibleBlock, node.transform, true);
                newBlock.transform.GetChild(0).gameObject.SetActive(false);
            }

            newBlock.transform.position = node.transform.position + new Vector3(0, 1, 0);
            referenceList.Add(newBlock);
            blokInfo.HasBlock = true;


        }

    }

    /// <summary>
    /// Generates the indestructible world ground with walls for each "x" line generates a block for each "z" value
    /// </summary>
    private void GenerateGround() {
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
    private void linkWithBlock(int[] dir, int pos) {
        
        if (WhoIs(dir) != -1 && dir != null)

            Graph.SetRelationShip(length, pos, WhoIs(dir));


    }


    /// <summary>
    /// this calculates who is up, down , left, or right depending on a Vector2 (x,y) return -1 if the direction is not possible
    /// (1,0) is right (0,1) is up
    /// </summary>
    private int WhoIs(int[] direction) {
        //  (-1,0)

        if (!ValidateDirection(direction, length))
            return -1;

        return Convert.ToInt32(length + direction[0] + direction[1] * widthAndHeight);

    }


    /// <summary>
    /// validates certain direction from given block number, THIS IS ONLY FOR MAP GENERATION
    /// you dont need to know how it works
    /// </summary>
    private bool ValidateDirection(int[] direction, int blockNumber) {

        //si estoy en un borde izquierdo y me preguntan por su izquierdo
        if (blockNumber % widthAndHeight == 0 && direction[0] < 0)
            return false;

        //si estoy en un borde derecho y me preguntan por su derecho        
        if ((blockNumber + 1) % widthAndHeight == 0 && direction[0] > 0)
            return false;

        //si estoy en el borde superior y me preguntan por el de arriba
        if (blockNumber - widthAndHeight < 0 && direction[1] < 0)
            return false;

        //si estoy en el borde inferior y me preguntan por el de abajo
        if (blockNumber + widthAndHeight > widthAndHeight * widthAndHeight && direction[1] > 0)
            return false;

        return true;

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
