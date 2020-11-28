
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class IAMovementController:Controller
{ 
        private Queue<GameObject> directions;
        private Queue<Quaternion> rotation;
        private float cubeSize = 2.1f;

        public Vector3 currentDir;
        public Quaternion currentRot;
        public int currentObjetive;
        private bool isMoving;
        

        private void Start()
        {
                base.Start();
                directions=new Queue<GameObject>();
                
        }

        private void AddMovement(int endpos)
        {
                int currentPosition=Int32.Parse(currentBlock.name);
                Debug.Log("The current position es "+currentPosition);
                GameObject[] objects = MyMap.getRoute(currentPosition, endpos);
                for (int i = 0; i < objects.Length; i++)
                {
                        directions.Enqueue(objects[i]);
                }

        }

        private void Update()
        {
                moving();
               if (Input.GetKey(KeyCode.K))
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
                       // Debug.Log("El vector goal es "+goal);
                       // Debug.Log("El vector current es "+currentPosition);
                        if (Vector3.Distance(currentPosition, goal) < 2.0) 
                        {
                                directions.Dequeue();
                        }
                        else
                        {
                                gameObject.transform.position = Vector3.MoveTowards(goal, currentPosition, velocidad);
                        }
                }
                
        }

        private void teinvocosatanas()
        {
                AddMovement(74);
        }
} 
