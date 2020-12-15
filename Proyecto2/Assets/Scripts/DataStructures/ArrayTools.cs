﻿using System.Collections.Generic;
using UnityEngine;

namespace DataStructures
{
    public class ArrayTools<T>
    {
        public static void initArray(T[] array, T data)
        {
            if (array == null) return;
            if (array.Length <= 0) return;
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = data;
            }
        }

        public static T[] getArrayFromStack(Stack<T> stack)
        {
            T[] array=new T[stack.Count];
            int i = 0;
            while (stack.Count > 0)
            {
                array[i] = stack.Pop();
                i++;
            }
            return array;
        }

        public static void Mix(T[] vector)
        {
            int randomPositionA;
            int randomPositionB;
            T backup;
            for (int i = 0; i < vector.Length; i++)
            {
                randomPositionA = Random.Range(0, vector.Length + 1)%vector.Length;
                randomPositionB=Random.Range(0, vector.Length + 1)%vector.Length;
                backup = vector[randomPositionA];
                vector[randomPositionA] = vector[randomPositionB];
                vector[randomPositionB] = backup;
            }
        }
    }
    
}