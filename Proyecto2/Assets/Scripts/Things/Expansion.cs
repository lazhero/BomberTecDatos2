
using Players;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Expansion : MonoBehaviour
{
    private float cubeSize=1.8f;
    private bool finishedCondition;
    public int ratio { set; get; } = 4;
    [SerializeField] private GameObject explosion;


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
        if (other.CompareTag("Player") ||other.CompareTag("Enemy"))
            other.GetComponent<PlayerHealth>().Health -= 1;
        
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
                    block.DestroyMe();
            finishedCondition = true;
        }
    }

  



}

