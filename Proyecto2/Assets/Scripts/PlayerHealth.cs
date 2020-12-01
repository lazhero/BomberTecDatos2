using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    
    public int totalHealth = 3;
    private int health;
    private float heartSize = 16f;

    public RectTransform heartUI;
    
    // Start is called before the first frame update
    void Start()
    {
        health = totalHealth;   
    }

    public void AddDamage(int amount)
    {
        health = health - amount;
        
        // Game  Over
        if (health <= 0) {
            health = 0;
        }

        heartUI.sizeDelta = new Vector2(heartSize * health, 14);
        Debug.Log("Player got damaged. His current health is " + health);
    }

    public void AddHealth(int amount)
    {
        health = health + amount;

        // Max health
        if (health > totalHealth) {
            health = totalHealth;
        }
        
        heartUI.sizeDelta = new Vector2(heartSize * health, 14);
        Debug.Log("Player got some life. His current health is " + health);
    }
}
