using UnityEngine;

namespace Effects
{
    public class HealthRestore : Effect
    {
        public float amount;
        public bool fullHeal;

        public override string GetName()
        {
            return "Health Restore";
        }

        public override string GetDescription()
        {
            return "Restores your health.";
        }

        public override void ApplyTo(GameObject obj)
        {
            if (obj.TryGetComponent(out Health health))
            {
                health.Heal(fullHeal ? health.MaxHealth : amount);
            }
        }

        public override void RemoveFrom(GameObject obj)
        {
        }
    }
}