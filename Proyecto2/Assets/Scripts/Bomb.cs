  using Things;
  using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosion;
    public Map map;
    public int pos;
    public float time;
    public int radio = 2;
    public BoxCollider bc;
    void Start()
    {
        bc = GetComponent<BoxCollider>();
        Invoke("Explote",time);
        bc.enabled = false;
        
        map.ThingChange(new message("Bomb",pos, message.Write));
        map.ThingChange(new message("Ratio",radio, message.Write));
        map.SendMessage("BlockClosed",pos);

        
    }
    /// <summary>
    ///  Destroy gameObject and generates the explosion
    /// </summary>
    public void Explote(){
        
        GameObject exp=Instantiate(explosion);
        exp.transform.position= transform.position;
        exp.GetComponent<Estela>().Ratio = radio  ;
        
        map.ThingChange(new message("Bomb",pos, message.Erase));
        map.ThingChange(new message("Ratio",radio, message.Erase));
        map.SendMessage("BlockDestroyed",pos);
        Destroy(gameObject);

    }

    
    
    
    
    
    
    private void OnTriggerExit(Collider other)
    {
        bc.enabled = true;
    }
}
