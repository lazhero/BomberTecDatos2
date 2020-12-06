using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Players
{
    public class PlayerHealth : MonoBehaviour
    {

        private bool canReciveDamage = true;
        public TextMeshProUGUI explosionRatio;
        public TextMeshProUGUI evasion;
        public Image face;
        public Image body;

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


        public int totalHealth = 3;
        public int health;
        public int Health
        {
            set
            {
                if (value < 0)
                {
                    canReciveDamage = false;
                    Invoke("becomeDamageAble",1f*Time.deltaTime);
                }

                if (canReciveDamage)
                {

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


            }
            get => health;
        }

        private float _heartSize = 16f;
        private float _shoeSize = 13.73899f;
        private float _shieldSize = 12.25836f;
        private float _bombSize = 12.25832f;

        public RectTransform heartUi;
        public RectTransform shoeUi;
        private Animator Anim;
        private static readonly int Death = Animator.StringToHash("DEATH");

        void Start()
        {
            Health = totalHealth;
            Anim = GetComponentInChildren<Animator>();
        }

        public void ModifyStats(int vida, int escudo, int bombas, int zapato)
        {
            Health += vida;
        }

        private void death()
        {
            Anim.SetBool(Death,true);
            Color color;
            ColorUtility.TryParseHtmlString("#2E2E2E", out color);
            face.color = color;
            body.color = color;
            
        }
        private void becomeDamageAble()
        {
            canReciveDamage = true;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Player")
            {
                Physics.IgnoreCollision(other.gameObject.GetComponent<BoxCollider>(),gameObject.GetComponent<BoxCollider>() );
            }
        }
    }
}
