using UnityEngine;

namespace DefaultNamespace
{
   
    public class ScoreTable:MonoBehaviour
    {
        [SerializeField]public float score { set; get; }
        public float shortestDistanceFromPlayer { set; get; }
        public int SuccessBombs { set; get; }
        
    }
}