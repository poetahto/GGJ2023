using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class MultiEffect : MonoBehaviour
    {
        public List<Effect> effects;

        public void ApplyAll(GameObject target)
        {
            foreach (var effect in effects)
            {
                effect.ApplyTo(target);
            }
        }

        public void RemoveAll(GameObject target)
        {
            foreach (var effect in effects)
            {
                effect.RemoveFrom(target);
            }
        }
    }
}