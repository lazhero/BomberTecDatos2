
using System;
using Players;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Expansion : MonoBehaviour
{
    private float cubeSize=1.8f;
    private bool finishedCondition;
    
    public int damage = 1;
    public string Owner { set; get; }
    public Vector3 direction { set; get; }
    public int ratio { set; get; } = 4;
    [SerializeField] private GameObject explosion;
    private Mendel mendel;

    private void Start()
    {
        mendel = GameObject.FindObjectOfType<Mendel>();
    }

    private void Update()
    {   
        if(ratio>0 && !finishedCondition)
        {
            ratio--;
            transform.Translate(Vector3.forward + Vector3.forward * (cubeSize * Time.deltaTime));
            Instantiate(explosion).transform.position = transform.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }

 
    

  
    private void OnTriggerEnter(Collider other)
    {
        int value = 0;
        if (other.CompareTag("Player") ||other.CompareTag("Enemy"))
        {
            other.GetComponent<PlayerHealth>().Health -= 1;
            if (other.name.CompareTo(Owner) != 0)
            {
                value = 50;
                mendel.updateValue(Int32.Parse(other.gameObject.name),-value );
            }
            else value = -300;

        }
        
        if(other.CompareTag("Bomb"))
            other.GetComponent<Bomb>().Explote();
            
        if(other.CompareTag("consumable"))
            other.GetComponent<Consumable>().Disapear();




        if (other.CompareTag("Wall"))
        {
            finishedCondition = true;
            return;
        }
        if (other.CompareTag("block") )
        {

            Instantiate(explosion).transform.position = transform.position;
            Block block = other.GetComponent<Block>();
            if(block!=null)
                if (block.isDestructible)
                {
                    block.DestroyMe();
                    value = 40;
                }
            finishedCondition = true;
            
        }
        mendel.updateValue(Int32.Parse(Owner),value);
    }

  



}

