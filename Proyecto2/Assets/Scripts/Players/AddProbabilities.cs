using System;
using UnityEngine;
namespace Players
{
    public class AddProbabilities:MonoBehaviour
    {
        private IAProbability probability;
        private void Start()
        {
            probability = gameObject.AddComponent<IAProbability>();
            probability.setBehaviorsNumber(4);
            probability.addBehavior(new Hide());
            probability.addBehavior(new FindEnemy());
            probability.addBehavior(new LeaveBomb());
            probability.addBehavior(new FindPowerUp());
        }
    }
}