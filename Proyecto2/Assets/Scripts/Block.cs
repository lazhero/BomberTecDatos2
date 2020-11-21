using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private bool isDestructible=false;
    [SerializeField] private GameObject dead_Object;
    private bool isDestroy=false;


    /**
     * @brief destroy the block 
     */
    void Destr() {
        Destroy(gameObject);
    }

    /**
     * @brief destroy the block and instantiate debris
     * @author Adrian Gonzalez
     */
    private void DestroyMe() {
        if (!isDestructible || isDestroy) return;
        
        isDestroy = true;
        
        var producto= Instantiate(dead_Object);
        producto.transform.position = transform.position;
                    
        Invoke("Destr",0.1f);

    }
}
