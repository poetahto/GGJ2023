using UnityEngine;

namespace Effects
{
    public class MaxHealthBoost : Effect
    {
        public float amount;
    
        public override void ApplyTo(GameObject obj)
        {
            if (obj.TryGetComponent(out Health health))
            {
                health.MaxHealth += amount;
            }
        }

        public override void RemoveFrom(GameObject obj)
        {
            if (obj.TryGetComponent(out Health health))
            {
                health.MaxHealth -= amount;
            }
        }
    }
}
