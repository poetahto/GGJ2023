using UnityEngine;

namespace Effects
{
    public class MaxHealthBoost : Effect
    {
        public float amount;

        public override string GetName() => "Stature";

        public override string GetDescription() => "Your maximum health is increased.";

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
