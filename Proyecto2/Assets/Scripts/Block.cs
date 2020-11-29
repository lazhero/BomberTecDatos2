using UnityEngine;

public class Block : MonoBehaviour
{

    public GameObject ham;

    [SerializeField]
    public bool isDestructible;
    
    [SerializeField] private GameObject dead_Object;
    private bool isDestroy=false;


    /// <summary>
    /// destroy the block 
    /// </summary>
    void Destr() {
        Destroy(gameObject);
    }

    /// <summary>
    /// destroy the block and instantiate debris
    /// </summary>
    public void DestroyMe() {
        if (!isDestructible || isDestroy) return;
        

        
        
        isDestroy = true;
        var producto= Instantiate(dead_Object);
        producto.transform.position = transform.position;
                    
        Invoke("Destr",0.1f);
        
        GameObject myHam = Instantiate(ham);
        ham.transform.position = gameObject.transform.position + new Vector3(0, 1.5f, 0);
        Debug.Log("Ham created");
    }

}
