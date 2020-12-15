using UnityEngine;

namespace Things
{
    public class Estela : MonoBehaviour
    {
        [SerializeField]
        private Expansion[] _expansions;
        public int Ratio { set; get; }
        public string Owner { get; set; }
        private void Start()
        {
            foreach (var e in _expansions)
            {
                e.Owner = Owner;
                e.ratio = Ratio;
            }
        }
    }
}
