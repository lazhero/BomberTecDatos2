using UnityEngine;

namespace Things
{
    public class Estela : MonoBehaviour
    {
        [SerializeField]
        private Expansion[] _expansions;
        public int Ratio { set; get; }

        private void Start()
        {
            foreach (var e in _expansions)
            {
                e.ratio = Ratio;
            }
        }
    }
}
