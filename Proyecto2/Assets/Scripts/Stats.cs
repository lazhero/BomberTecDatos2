using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{

    public int speed;
    public int life;
    public int dodge;
    public int expRatio;
    
    // Start is called before the first frame update
    void Start()
    {
        setStats();
    }

    void setStats()
    {
        speed = Random.Range(4, 7);
        life = Random.Range(3, 6);
        expRatio = Random.Range(3, 6);
        dodge = 20 - speed - life - expRatio; // 20 is the sum of all the stats
    }
    
}
