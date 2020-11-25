
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class IAMovementController:Controller
{ 
        private Queue<int[]> directions;
        private Queue<Quaternion> rotation;
        private float cubeSize = 2.1f;

        public Vector3 currentDir;
        public Quaternion currentRot;
        public int currentObjetive;
        private bool isMoving;
        
        private static int[] UP    = {0, -1};
        private static int[] DOWN  = {0, 1};
        private static int[] RIGHT = {1, 0};
        private static int[] LEFT  = {-1, 0};
        
        
        
        private void Start()
        {
            base.Start();
            directions=new Queue<int[]>();
            rotation=new Queue<Quaternion>();
            velocidad = 2;

                
            
            
            
           
        }

        public void c()
        {
            isMoving = true;
            
            Addmove(UP);
            Addmove(UP);
            Addmove(RIGHT);
            Addmove(RIGHT);
            Addmove(DOWN);
            Addmove(DOWN);
            Addmove(LEFT);
            Addmove(LEFT);

        
            ChangeMove();

        }
        
        private void Update() {
            
           if (Input.GetKeyDown("i")) c();

           if (!(currentBlock ?? false)) return;
           isMoving = directions.Count > 0;

            if (isMoving)
            {
                  
                if (isDone()) ChangeMove();
                else
                    Move(currentDir, currentRot);
            }
            else
            {
                anim.SetBool("MOVING", false);
            }

            if (isDone()) ChangeMove();

        }

        private bool isDone()
        {
            return Convert.ToInt32( currentBlock.name) == currentObjetive;

        }
        
        
        /// <summary>
        /// Adds the movement specification to the enqueue 
        /// </summary>
        /// <param name="direction"></param>
        private void Addmove(int[] direction)
        {
            var algo =  Math.Abs(direction[1])*(Math.Abs(1 - direction[1])) / 2;
            var q = Quaternion.Euler(0, -direction[0] * 90 + algo*180, 0);
            directions.Enqueue(direction);
            rotation.Enqueue(q);
        }

      
        /// <summary>
        /// 
        /// </summary>
        private void ChangeMove()
        {
            var vector = directions.Dequeue();
            currentRot =  rotation.Dequeue();
            currentDir =new Vector3(-vector[0],0,vector[1]);

            currentObjetive=MyMap.DetectWalkable(Convert.ToInt32(currentBlock.name),vector);
        }

        public void stopMovementSequence()
        {
            rotation.Clear();
            directions.Clear();
        }


} 
