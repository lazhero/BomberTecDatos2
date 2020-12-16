using System;
using UnityEngine;

namespace DefaultNamespace
{
   
    public class ScoreTable:MonoBehaviour
    {
        [SerializeField] public float score { set; get; } = 0;
        public float shortestDistanceFromPlayer { set; get; } = Int32.MaxValue;
        public int SuccessBombs { set; get; } = 0;

        public void resetToClean()
        {
            score = 0;
            shortestDistanceFromPlayer = Int32.MaxValue;
            SuccessBombs = 0;
        }

    }
}