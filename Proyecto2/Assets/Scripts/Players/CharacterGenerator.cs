﻿using System;
using DefaultNamespace;
using DefaultNamespace.Configuration;
using Players.Behaviors;
using SquaredMapTools;
using UnityEngine;
using Genetics;
using Things;
using UnityEditor;
using Random = UnityEngine.Random;

namespace Players
{
    public class CharacterGenerator : MonoBehaviour
    {
        [SerializeField] public  AiBehavior[] AiBehaviors;
        [SerializeField] private  GameObject playerPrefab;
        [SerializeField] private int humanPlayers;
        [SerializeField] private int maxPlayers;
        [SerializeField] private GameObject bomba;
        [SerializeField] private GameObject PlayerHud;
    
        private static GameObject Static_HUD;
        private static GameObject Static_BombPrefab;
        private static GameObject Static_GeneralCanva;
        private static GameObject Static_Player;
        private static Mendel geneticFather;
        private static int Static_HumansCuantity;
        private static  AiBehavior[] aibehaviors;
        private static int maximum;
    
        private void Awake()
        {
            Static_BombPrefab   = bomba;
            Static_HUD          = PlayerHud;
            Static_Player       = playerPrefab;
            Static_HumansCuantity = humanPlayers;
            Static_GeneralCanva = GameObject.FindGameObjectWithTag("Canva");
            geneticFather       = GameObject.FindGameObjectWithTag("Mendel").GetComponent<Mendel>();
            aibehaviors           = AiBehaviors;
            maximum = maxPlayers;
            int h = MapConfig.Humans;
            if (h > 1)
            {
                Static_HumansCuantity = h;
            }
            
        }


        /// <summary>
        /// Sets all defaultConfig for all players
        /// </summary>
        /// <param name="player"></param>
        /// <param name="isAHuman"></param>
        private static void DefineDefaultSettings(GameObject player,bool isAHuman) {
        
        
            var health =player.GetComponent<PlayerHealth>();
            var pHud  = Instantiate(Static_HUD , Static_GeneralCanva.transform).GetComponent<HudParts>();
            Color myColor= new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        
            pHud.colorID.color = myColor;
            pHud.AIMarker.SetActive(!isAHuman);  
        
            health.heartUi        = pHud.Health;
            health.explosionRatio = pHud.ExplosionRatio;
            health.shoeUi         = pHud.Velocity;
            health.evasion        = pHud.Evasion;
            health.face           = pHud.Face;
            health.body           = pHud.colorID;
            health.velocidad = pHud.velocityText;
            health.powerUpImage = pHud.powerUp;
            health.powerUpText = pHud.powerUpText;
            player.GetComponent<Controller>().bomba = Static_BombPrefab;
            player.transform.GetComponentInChildren<MeshRenderer>().material .SetColor("_Color",myColor);
          

        }
    
    
       /// <summary>
        /// Generates a new one player with certain position
        /// </summary>
        /// <param name="humanController"></param>
        /// <param name="trans"></param>
        private static void GeneratePlayer(bool humanController, GameObject trans,string name)
        {
            var player= Instantiate(Static_Player);
            player.name = name;
            if(humanController)
            {
                if (Static_HumansCuantity > 1)
                    player.AddComponent<JoystickController>();
                else
                    player.AddComponent<PlayerController>();


            }
            else
            {
                player.AddComponent<IAMovementController>();

                foreach (Component VARIABLE in aibehaviors)
                {
                    ComponentUtil.CopyComponent(VARIABLE,player);
                
                    //UnityEditorInternal.ComponentUtility.CopyComponent(VARIABLE);
                    //UnityEditorInternal.ComponentUtility.PasteComponentAsNew(player);
                }

                player.AddComponent<IAProbability>().setBehaviorsNumber(aibehaviors.Length);
                player.AddComponent<ScoreTable>();
                player.tag = "Enemy";
                geneticFather.AddNewBean(player);
            }
            
            
            player.transform.position = trans.transform.position+ new Vector3(0,3,0);
            DefineDefaultSettings(player,humanController);
            
        }
    
    
        /// <summary>
        /// Generate players foreach spawn zone
        /// </summary>
        /// <param name="n" >Side measure</param>
        /// <param name="nodes"></param>
        public static void GenerateAllPlayers( int n, DGraph<GameObject> nodes)
        {
            //gets all map spawn zones
            //initialize the genetic algorithm population array
            
            
            int[] spawns = PositionTools.DetermineSpawns(n);
            geneticFather.Being = new GameObject[maximum-Static_HumansCuantity];
            geneticFather.startPos = Static_HumansCuantity;
            for (var i=0; i< spawns.Length&& i<maximum;i++ )
            {
             
                GeneratePlayer(Static_HumansCuantity>0, nodes.getNode(spawns[i]), i.ToString());
                Static_HumansCuantity--;
            }
            geneticFather.init();
        }
    
    }
}