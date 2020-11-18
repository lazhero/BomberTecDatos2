using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGenerator : MonoBehaviour
{
   
    public GameObject bloque;
    public int ancho;
    public int lenght=0;
    public int altura;
    public float blockSize;
    void Start()
    {
        for (int x=0;x<ancho;x++){
                for (int z= 0; z<altura;z++){

                    GameObject newBlock=Instantiate(bloque);
                    newBlock.transform.parent= gameObject.transform;// le asigno un objeto padre
                    newBlock.transform.position=new Vector3(x*blockSize, 0, z*blockSize);  //establezco a que distancia esta
                    newBlock.transform.eulerAngles=new Vector3(90,0,0);  //lo roto
                    newBlock.gameObject.name= lenght.ToString();
                    lenght++;

                }
                 
            }
    }


    void Update()
    {
        
    }
}
