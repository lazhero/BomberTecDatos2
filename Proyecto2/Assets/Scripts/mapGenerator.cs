using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGenerator : MonoBehaviour
{
    
    public GameObject Ground_block;
    public GameObject Wall_block;
    
    [SerializeField]
    private int widthAndHeight=10;
    [SerializeField]
    private int lenght=0;
    [SerializeField]
    private float blockSize=2.1f;
    
  
    
    private void generateBlock(GameObject newObject, int x,int y, int z){
        
        GameObject newBlock=Instantiate(newObject,gameObject.transform);       //Instancio el objeto y le asigno de padre el mapa
        newBlock.transform.position=new Vector3(x*blockSize, 0, z*blockSize);  //establezco a que distancia esta
        newBlock.gameObject.name= lenght.ToString();                           //le cambio el nombre
    }   
    

    public void generateGround(){
         for (int x=0;x<widthAndHeight;x++){
             
                for (int z= 0; z<widthAndHeight;z++){

                    if(x==0 || x== widthAndHeight-1){
                        generateBlock(Wall_block,x,0,z);

                    }
                    else if(z==0 || z==widthAndHeight-1){
                           generateBlock(Wall_block,x,0,z);

                    }
                    else{
                        generateBlock(Ground_block,x,0,z);
                    }
                    lenght++;

                }     
        }
    }

    void Start()
    {
       generateGround();

    }


    void Update()
    {
        
    }
}
