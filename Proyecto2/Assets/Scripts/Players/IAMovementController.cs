﻿
using System;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class IAMovementController:Controller
{ 
        private Queue<GameObject> directions;
        private float cubeSize = 2.1f;
        public int currentObjetive;
        public List<int> DEBUG;

        //?--------------------------------->
        //!--------------------------------->
        //?--------------------------------->

        private void Start()
        {
                base.Start();
                directions=new Queue<GameObject>();      
        }

  
        public void AddMovement(int endpos)
        {
                var currentPosition=Int32.Parse(currentBlock.name);
                GameObject[] objects = myMap.GetRoute(currentPosition, endpos);
                if (objects == null) return;
                if (objects.Length <= 1) return;
                foreach (var t in objects)
                {
                        directions.Enqueue(t);
                }
        }

        public void emptyMovement()
        {
                directions.Clear();
        }


        
        private void Update()
        {
                moving();
               if (Input.GetKeyDown(KeyCode.K))
               { 
                       teinvocosatanas();
               }

        }

        private void moving(){
                if (directions.Count > 0)
                {
                        Vector3 goal = directions.Peek().transform.position;
                        Vector3 currentPosition = gameObject.transform.position;
                        goal.y = currentPosition.y;

                        if (Vector3.Distance(currentPosition, goal) < 0.05) 
                                directions.Dequeue();
                        else
                                Move(Vector3.Normalize(goal - currentPosition));
                }
                else
                {

                        Anim.SetBool("MOVING",false);
                }
                
        }

        private void teinvocosatanas()
        {
                AddMovement(currentObjetive);
        }

        public bool Ismoving()
        {
                return directions.Count > 0;
        }
        
} 
