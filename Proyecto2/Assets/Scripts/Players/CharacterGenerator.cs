using System;
using Players;
using SquaredMapTools;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterGenerator : MonoBehaviour
{

    [SerializeField] private  GameObject playerPrefab;
    [SerializeField] private int humanPlayers;
    [SerializeField] private GameObject bomba;
    [SerializeField] private GameObject PlayerHud;
    
    private static GameObject Static_HUD;
    private static GameObject Static_BombPrefab;
    private static GameObject Static_GeneralCanva;
    private static GameObject Static_Player;
    private static int Static_HumansCuantity;
    
    
    private void Awake()
    {
        Static_Player = playerPrefab;
        Static_HumansCuantity = humanPlayers;
        Static_BombPrefab = bomba;
        Static_HUD = PlayerHud;
        Static_GeneralCanva = GameObject.FindGameObjectWithTag("Canva");
        
    }
    /// <summary>
    /// Sets all defaultConfig for all players
    /// </summary>
    /// <param name="player"></param>
    private static void DefineDefaultSettings(GameObject player,bool isAHuman)
    {
        var health =player.GetComponent<PlayerHealth>();
        var pHud  = Instantiate(Static_HUD , Static_GeneralCanva.transform).GetComponent<HudParts>();
        
        Color myColor= new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        pHud.colorID.color = myColor;
        pHud.AIMarker.SetActive(!isAHuman);  
        health.heartUi=pHud.Health;
        health.explosionRatio = pHud.ExplosionRatio;
        health.shoeUi = pHud.Velocity;
        health.evasion = pHud.Evasion;
        health.face = pHud.Face;
        health.body = pHud.colorID;
        
        player.GetComponent<Controller>().bomba = Static_BombPrefab;
        player.transform.GetComponentInChildren<MeshRenderer>().material .SetColor("_Color",myColor);

    }
    
    /// <summary>
    /// Generates a new one player with certain position
    /// </summary>
    /// <param name="humanController"></param>
    /// <param name="trans"></param>
    private static void GeneratePlayer(bool humanController, GameObject trans)
    {
        var player= Instantiate(Static_Player);
            
        if(humanController) 
            player.AddComponent<PlayerController>();
        else
        {
            player.AddComponent<IAMovementController>();
        }
        player.transform.position = trans.transform.position+ new Vector3(0,3,0);
        DefineDefaultSettings(player,humanController);
    }
    /// <summary>
    /// Generate players foreach spawnzone
    /// </summary>
    /// <param name="n" Side measure></param>
    /// <param name="nodes"></param>
    public static void GenerateAllPlayers( int n, DGraph<GameObject> nodes)
    {

        foreach (var nodeName in PositionTools.DetermineSpawns(n))
        {
            GeneratePlayer(Static_HumansCuantity>0, nodes.getNode(nodeName));
            Static_HumansCuantity--;
        }
    }
    
}