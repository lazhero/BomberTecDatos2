using System;
using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    protected Animator Anim;
    private GameObject _body;
    public float velocidad = 5f;
    public GameObject currentBlock;
    [SerializeField]
    public GameObject bomba;
    public Map myMap;
    private bool _canPutABomb=true;
    private float _bombTime;
    private PlayerHealth _stats;
    private Rigidbody rbbody;
    private static readonly int Moving = Animator.StringToHash("MOVING");
    [SerializeField]
    private bool DebugMode=true;
    //?--------------------------------->
    //!--------------------------------->
    //?--------------------------------->
    
    protected void Start()
    {
        myMap = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        _body = gameObject.transform.GetChild(0).gameObject;
        Anim = _body.GetComponent<Animator>();
        _bombTime = bomba.GetComponent<Bomb>().time;
        _stats = GetComponent<PlayerHealth>();
        rbbody = GetComponent<Rigidbody>();

    }
    /// <summary>
    /// moves the player
    /// </summary>
    /// <param name="dir"></param>
    protected void Move(Vector3 dir)
    {
        var x = dir[0]*90 + ((int) (1 - dir[2]) / 2) * 180;
        var angle = Quaternion.Euler(0,x , 0);
        
        Anim.SetBool(Moving, true);
        //gameObject.transform.Translate(dir * (velocidad * Time.deltaTime),Space.World);
        //rbbody.velocity = _body.transform.forward*velocidad;
        rbbody.MovePosition(transform.position+dir * (velocidad * Time.deltaTime));
        _body.transform.rotation = angle;

    }
    /// <summary>
    /// enables capabilty to put bombs
    /// </summary>
    private void CanPutAgain()
    {
        _canPutABomb = true;
    }
    /// <summary>
    /// Generates bombs
    /// </summary>
    public void GenerateBomb()
    {
        if(_canPutABomb || DebugMode)
        {
            GameObject bombita = Instantiate(bomba);
            bombita.transform.position = currentBlock.transform.position + new Vector3(0, 1.5f, 0);
            _canPutABomb = false;
            
            Bomb info=bombita.GetComponent<Bomb>();
            info.map = myMap;
            info.pos = Convert.ToInt32(currentBlock.name);
            info.radio = _stats.BombRatio;
            
            Invoke("CanPutAgain",_bombTime);

        }
    
        
    }

}

