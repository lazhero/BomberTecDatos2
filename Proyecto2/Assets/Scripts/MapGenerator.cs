using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject groundBlock;
    [SerializeField] private GameObject wallBlock;
    [SerializeField] private int widthAndHeight = 10;
    [SerializeField] private int length;
    [SerializeField] private float blockSize = 2.1f;
    public DGraph<GameObject> Graph { get; set; }

    private int[] UP = {0, -1};
    private int[] DOWN = {0, 1};
    private int[] RIGHT = {1, 0};
    private int[] LEFT = {-1, 0};

    public bool DebugMode = false;

    private int normalCost = 10;

    void Start()
    {
        Graph = new DGraph<GameObject>(widthAndHeight * widthAndHeight);
        Graph.startAllWith(10);
        generateGround();

    }


    /**
     * @brief instantiate one Block of newObject prefab Specified, with x,y,z coordinates and asociate it with the graph
     * @param1 int
     * @param2 int
     * @param3 int
     */
    private void generateBlock(GameObject newObject, int x, int y, int z)
    {

        GameObject newBlock = Instantiate(newObject, gameObject.transform); //Instancio el objeto y le asigno de padre el mapa
        newBlock.transform.position = new Vector3(x * blockSize, 0, z * blockSize); //establezco a que distancia esta
        newBlock.gameObject.name = length.ToString(); //le cambio el nombre
        // ! DebugThing ERASE LATER --------------------------------------------------->
        if (DebugMode)
        {
            newBlock.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = length.ToString();    
        }
        else
        {
            newBlock.transform.GetChild(0).gameObject.SetActive(false);
            
        }
        //! --------------------------------------------------->
        Graph.setNodeData(length, newBlock);

        linkWithBlock(UP, 0);
        linkWithBlock(DOWN, 1);
        linkWithBlock(LEFT, 2);
        linkWithBlock(RIGHT, 3);

        //!Debug.Log("arriba de " + length + "esta " + whoIs(UP));
        //!Debug.Log("debajo de " + length + "esta " + whoIs(DOWN));
        Debug.Log("derecha de " + length + "esta " + whoIs(RIGHT));
        Debug.Log("izquierda de " +length +"esta "+ whoIs(LEFT));
     
        
    }
    /**
     * @brief associate block with the GRAPH and asociate it with is neighboors 
     */
    private void linkWithBlock(int[] dir,int pos) {
        if (whoIs(dir) != null && dir !=null)
            
            Graph.SetRelationShip(length, pos, whoIs(dir));
     
        
    }
    
    /**
     * @brief generate the indestructible world ground with walls for each "x" line generates a block for each "z" value
     * @Author Adrian Gonzalez
     */
    private void generateGround(){
         for (var z=0;z<widthAndHeight;z++){
             
                for (int x= 0; x<widthAndHeight;x++){

                    if(x==0 || x== widthAndHeight-1 || z==0 || z==widthAndHeight-1)
                        generateBlock(wallBlock,-x,0,z);
                    
                    else
                        generateBlock(groundBlock,-x,0,z);
                    
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
     */
    private int whoIs(int[] direction) { //  (-1,0)
    
        if (!validateDirection(direction, length))
        {
            return -1;
        }
     
            return Convert.ToInt32( length + direction[0] + direction[1] * widthAndHeight );
        
                
        

        

        
    }
    /**
     * @brief validates certain direction from given block number, THIS IS ONLY FOR MAP GENERATION
     * you dont need to know how it works
     * @param1 Vector2 up, down, left or right 
     * @param2 float the current blockNumber who is asking
     * @return bool
     */
    private bool validateDirection(int[] direction, int blockNumber) {
        
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
