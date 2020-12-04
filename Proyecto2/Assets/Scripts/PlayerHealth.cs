using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    private bool canReciveDamage=true;
    
    public int totalBomb = 1;
    private int _bomb;
    

    private int Bomb
    {
        get => _bomb;
        set
        {
            Bomb =  value;
            if (Bomb > totalBomb)
            {
                Bomb = totalBomb;
                bombUi.sizeDelta = new Vector2(_bombSize * Bomb, 14.48443f);
            }

            if (Bomb <= 0)
            {
                Bomb = 0;
                bombUi.sizeDelta = new Vector2(Bomb * Bomb, 14.48443f);
            }


        }
    }

    public int totalShoe = 3;
    private int _shoe;

    private int Shoe
    {

        get => _shoe;
        set
        {
            Shoe = value;

            if (Shoe > totalShoe)
                Shoe = totalShoe;
            if (Shoe <= 0)
                Shoe = 0;
            shoeUi.sizeDelta = new Vector2(_shoeSize * Shoe, 13.00002f);

        }

    }

    public int totalShield = 3;
    private int _shield;

    private int Shield
    {
        set
        {
            Shield = value;

            if (Shield > totalShield)
                Shield = totalShield;

            if (Shield <= 0)
                Shield = 0;

            shieldUi.sizeDelta = new Vector2(_shieldSize * Shield, 15.9688f);

        }

        get => _shield;
    }


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
    private float _shoeSize = 13.73899f;
    private float _shieldSize = 12.25836f;
    private float _bombSize = 12.25832f;

    public RectTransform heartUi;
    public RectTransform shieldUi;
    public RectTransform shoeUi;
    public RectTransform bombUi;

    void Start()
    {
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
