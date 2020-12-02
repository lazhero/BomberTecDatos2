using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class IAProbability : MonoBehaviour
{

    private int MoveProb           { set; get; }= 20;
    private int BombProb           { set; get; }= 20;
    private int FollowProb         { set; get; }= 20;
    private int HideProb           { set; get; }= 20;
    private int SearchPowerUpsProb { set; get; }= 20;
    private IAMovementController movement;

    private void Start()
    {
        movement = gameObject.GetComponent<IAMovementController>() ;
    }

    private void FollowPlayer() {
        
    }
    private void SearchForPower() {
        
    }

    private void Hide() {
        
    }
    private void Move() {
        
        
    }



    private bool InRange(int totalRange,int ranNumb, int prob)
    {
        return ranNumb > totalRange && ranNumb < totalRange + prob;

    }
    public void RandomAction()
    {

        var ranNumb = Random.Range(0,100);
        var totalRange = 0;
        
        if(InRange(totalRange,ranNumb,MoveProb))
        {
            
        }

        totalRange += MoveProb;
        if(InRange(totalRange,ranNumb,BombProb))
        {
            
        }
        totalRange += BombProb;
        if(InRange(totalRange,ranNumb,HideProb))
        {
            Hide();
        }
        totalRange += HideProb;
        if(InRange(totalRange,ranNumb,SearchPowerUpsProb))
        {
            SearchForPower();
        }
        totalRange += SearchPowerUpsProb;
        if(InRange(totalRange,ranNumb,FollowProb))
            FollowPlayer();
      
    }
    void Update()
    {
        
    }
}
