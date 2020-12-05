using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    private bool canReciveDamage=true;

    public int totalHealth;
    public int health;
    public int Health
    {
        set
        {
            if (value < 0)
            {
                canReciveDamage = false;
                Invoke("becomeDamageAble",0.1f);
            }

            if (!canReciveDamage) return;
            
            health  = value;
            if (health > totalHealth)
                health = totalHealth;

            if (health <= 0)
                health = 0;

            heartUi.sizeDelta = new Vector2(_heartSize * health, 14);


        }
        get => health;
    }

    private float _heartSize = 16f;
    private float _shoeSize = 14.6f;
    private float _shieldSize = 12.25836f;
    private float _bombSize = 12.25832f;
    private float _expSize = 10f;

    public RectTransform heartUi;
    public RectTransform shieldUi;
    public RectTransform shoeUi;
    public RectTransform bombUi;

    public int totalShoe;
    public int Shoe;
    
    void Start()
    {
        totalShoe = gameObject.GetComponent<Stats>().speed;
        Shoe = totalShoe;
        shoeUi.sizeDelta = new Vector2(_shoeSize * Shoe, 13.00002f);
        totalHealth = gameObject.GetComponent<Stats>().life;
        Health = totalHealth;
    }

    public void ModifyStats(int vida, int escudo, int bombas, int zapato)
    {
        Health += vida;
    }


    private void becomeDamageAble()
    {
        canReciveDamage = true;
    }
    
    



}
