﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Players
{
    public class PlayerHealth : MonoBehaviour
    {

        public bool _canReceiveDamage = true;
        public TextMeshProUGUI explosionRatio;
        public TextMeshProUGUI evasion;
        public Image face;
        public Image body;

        public TextMeshProUGUI powerUpText;
        public Image powerUpImage { set; get; }
        
        
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

        private int _bombRatio;
        public int PreviusbombRatio;
        public int BombRatio
        {
            get => _bombRatio;
            set
            {
                _bombRatio = value;
                explosionRatio.text = _bombRatio.ToString();
            }
        }

        public int _evasion=0;
        public int Evasion
        {
            set
            {

                _evasion = value;
                
                if (_evasion > 90)
                    _evasion = 90;

                evasion.text = _evasion+ "%";


            }
            get => _evasion;
        }

        public int totalHealth = 3;
        public int health;
        public int Health
        {
            set
            {

                if (health > value && _canReceiveDamage)
                {
                    int random = Random.Range(0, 100);

                    if(random>=_evasion)
                        health = value;
                    _canReceiveDamage = false;
                    Invoke("BecomeDamageAble", 2f * Time.deltaTime);
                }
                
            
                if(health < value)
                    health = value;
                if (health > totalHealth)
                    health = totalHealth;

                if (health <= 0)
                {
                    death();
                    health = 0;
                }

                heartUi.sizeDelta = new Vector2(_heartSize * health, 14);


            }
            get => health;
        }

        private float _heartSize = 16f;
        private float _shoeSize = 13.73899f;
        private float _shieldSize = 12.25836f;

        public RectTransform heartUi;
        public RectTransform shoeUi;
        private Animator Anim;
        private static readonly int Death = Animator.StringToHash("DEATH");

        void Start()
        {
            Health = totalHealth;
            Anim = GetComponentInChildren<Animator>();
            BombRatio = 2;
        }
        /// <summary>
        /// Modify Stats
        /// </summary>
        /// <param name="vida"></param>
        /// <param name="escudo"></param>
        /// <param name="bombas"></param>
        /// <param name="zapato"></param>
        public void ModifyStats(int vida, int bombas,int shield)
        {
            Health += vida;
            BombRatio += bombas;
        }
        
        
        /// <summary>
        /// kills the player
        /// </summary>
        private void death()
        {
            Anim.SetBool(Death,true);
            Color color;
            ColorUtility.TryParseHtmlString("#2E2E2E", out color);
            face.color = color;
            body.color = color;
            
        }
        
        /// <summary>
        /// Allows player to receive damage
        /// </summary>
        private void BecomeDamageAble()
        {
            _canReceiveDamage = true;
        }

        
        
        
        
        
        
        
        
        
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Player" || other.gameObject.tag=="Enemy")
            {
                Physics.IgnoreCollision(other.gameObject.GetComponent<BoxCollider>(),gameObject.GetComponent<BoxCollider>() );
            }
        }
    }
}
