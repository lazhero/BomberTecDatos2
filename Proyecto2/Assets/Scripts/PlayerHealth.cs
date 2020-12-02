using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int totalBomb = 1;
    private int bomb;
    
    public int totalShoe = 3;
    private int shoe;
    
    public int totalShield = 3;
    private int shield;
    
    public int totalHealth = 3;
    private int health;
    
    private float heartSize = 16f;
    private float shoeSize = 13.73899f;
    private float shieldSize = 12.25836f;
    private float bombSize = 12.25832f;

    public RectTransform heartUI;
    public RectTransform shieldUI;
    public RectTransform shoeUI;
    public RectTransform bombUI;
    
    // Start is called before the first frame update
    void Start()
    {
        health = totalHealth;
        shoe = 0;
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
    
    public void AddShield(int amount)
    {
        shield = shield + amount;

        // Max health
        if (shield > totalShield) {
            shield = totalShield;
        }
        shieldUI.sizeDelta = new Vector2(shieldSize * shield, 15.9688f);
    }
    
    public void AddShoe(int amount)
    {
        Debug.Log("Adding shoe");
        shoe = shoe + amount;

        // Max health
        if (shoe > totalShoe) {
            shoe = totalShoe;
        }
        shoeUI.sizeDelta = new Vector2(shoeSize * shoe, 13.00002f);
    }
    
    public void AddBomb(int amount)
    {
        bomb = bomb + amount;

        // Max health
        if (bomb > totalBomb) {
            bomb = totalBomb;
        }
        bombUI.sizeDelta = new Vector2(bombSize * bomb, 14.48443f);
    }
    
    public void removeShoe(int amount)
    {
        shoe = shoe - amount;
        if (shoe <= 0) {
            shoe = 0;
        }
        shoeUI.sizeDelta = new Vector2(shoe * shoe, 13.00002f);
    }
    
    public void removeBomb(int amount)
    {
        bomb = bomb - amount;
        if (bomb <= 0) {
            bomb = 0;
        }
        bombUI.sizeDelta = new Vector2(bomb * bomb, 14.48443f);
    }
    
    public void removeShield(int amount)
    {
        shield = shield - amount;
        if (shield <= 0) {
            shield = 0;
        }
        shieldUI.sizeDelta = new Vector2(shield * shield, 15.9688f);
    }
    
    public void removeHealth(int amount)
    {
        health = health - amount;
        
        // Game  Over
        if (health <= 0) {
            health = 0;
        }

        heartUI.sizeDelta = new Vector2(heartSize * health, 14);
        Debug.Log("Player got damaged. His current health is " + health);
    }
}
