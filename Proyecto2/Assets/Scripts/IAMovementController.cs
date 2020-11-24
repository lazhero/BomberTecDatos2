
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class IAMovementController:Controller
{ 
        public Map MyMap { set; get; }
        public GameObject[] Gameobjects;
        private Queue<int[]> directions;
        private Queue<Quaternion> rotation;
        private float cubeSize = 2.1f;
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
        private void Update()
        {
           makeMove();
           Addmove(UP);
        }
        
        public bool isDone()
        {
            if (directions.Count > 0) return false;
            return true;
        }
        private void Addmove(int[] direction)
        {
            Quaternion q = Quaternion.Euler(0, -direction[0] * 90 + (1 - direction[0]) * 180, 0);
            directions.Enqueue(direction);
            rotation.Enqueue(q);
        }

        private void makeMove()
        {
            if (!isDone())
            {
                int[] vector = directions.Dequeue();
                Vector3 movement=new Vector3(vector[0],0,vector[1]);
                Quaternion q = rotation.Dequeue();
                Move(cubeSize*movement,q);
            }
        }

        public void stopMovementSequence()
        {
            rotation.Clear();
            directions.Clear();
        }


} 
