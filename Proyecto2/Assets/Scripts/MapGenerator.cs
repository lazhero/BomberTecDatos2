using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject groundBlock;
    [SerializeField] private GameObject wallBlock;
    [SerializeField] private int widthAndHeight = 10;
    [SerializeField] private int length;
    [SerializeField] private float blockSize = 2.1f;
    [SerializeField] private bool debugMode = false;
    [SerializeField] private int normalCost = 10;

    public DGraph<GameObject> Graph { get; set; }

    private int[] UP = {0, -1};
    private int[] DOWN = {0, 1};
    private int[] RIGHT = {1, 0};
    private int[] LEFT = {-1, 0};



    void Start() {
        Graph = new DGraph<GameObject>(widthAndHeight * widthAndHeight);
        Graph.startAllWith(10);
        GenerateGround();

    }


    /**
     * @brief instantiate one Block of newObject prefab Specified, with x,y,z coordinates and asociate it with the graph
     * @param1 int
     * @param2 int
     * @param3 int
     */
    private void GenerateBlock(GameObject newObject, int x, int y, int z)
    {
        // Instancio el objeto y le asigno de padre el mapa
        var newBlock = Instantiate(newObject, gameObject.transform); 
        //establezco a que distancia esta
        newBlock.transform.position = new Vector3(x * blockSize, 0, z * blockSize); 
        //le cambio el nombre
        newBlock.gameObject.name = length.ToString(); 
        
        
        Graph.setNodeData(length, newBlock);
        //Uno el nodo con sus nodos circundantes
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
    
    /**
     * @brief associate block with the GRAPH and asociate it with is neighboors
     * @param1 int[]
     * @param2 int 
     */
    private void linkWithBlock(int[] dir,int pos) {
        if (WhoIs(dir) != -1 && dir !=null)
            
            Graph.SetRelationShip(length, pos, WhoIs(dir));
     
        
    }
    
    /**
     * @brief generate the indestructible world ground with walls for each "x" line generates a block for each "z" value
     * @Author Adrian Gonzalez
     */
    private void GenerateGround(){
         for (var z=0;z<widthAndHeight;z++){
             
                for (var x= 0; x<widthAndHeight;x++){

                    if(x==0 || x== widthAndHeight-1 || z==0 || z==widthAndHeight-1)
                        GenerateBlock(wallBlock,-x,0,z);
                    
                    else
                        GenerateBlock(groundBlock,-x,0,z);
                    
                    length++;

                }     
         }
    }

    
    /**
     * @ brief this calculates who is up, down , left, or right depending on a Vector2 (x,y)
     * (1,0) is right (0,1) is up
     * @param1 Vector2 up, down, left or right 
     * @param2 float the current blockNumber who is asking
     * @return nullable int  it means that it can also return null
     * @return -1 if the direction is not posible
     */
    private int WhoIs(int[] direction) { //  (-1,0)
    
        if (!ValidateDirection(direction, length))
            return -1;
     
        return Convert.ToInt32( length + direction[0] + direction[1] * widthAndHeight );
        
    }
    
    
    /**
     * @brief validates certain direction from given block number, THIS IS ONLY FOR MAP GENERATION
     * you dont need to know how it works
     * @param1 Vector2 up, down, left or right 
     * @param2 float the current blockNumber who is asking
     * @return bool
     */
    private bool ValidateDirection(int[] direction, int blockNumber) {
        
        //si estoy en un borde izquierdo y me preguntan por su izquierdo
        if (blockNumber % widthAndHeight == 0  && direction[0] < 0 )
                return false;
        
        //si estoy en un borde derecho y me preguntan por su derecho        
        if((blockNumber + 1) % widthAndHeight == 0&& direction[0] > 0)
                return false;
        
        //si estoy en el borde superior y me preguntan por el de arriba
        if (blockNumber - widthAndHeight < 0 && direction[1] < 0)
            return false;
        
        //si estoy en el borde inferior y me preguntan por el de abajo
        if (blockNumber + widthAndHeight > widthAndHeight*widthAndHeight && direction[1] > 0)
            return false;
        
        return true;    
            
    }

   

}
