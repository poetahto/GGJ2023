using UnityEngine;

namespace Effects
{
    public class StandStillDamage : Effect
    {
        public float minSpeed = 0.1f;
        public float damagePerSecond = 1;

        public override string GetName() => "Abhor";

        public override string GetDescription() => "Standing still deals damage.";

        public override void ApplyTo(GameObject obj)
        {
            var applier = obj.AddComponent<StandStillDamageApplier>();
            applier.MinSpeed = minSpeed;
            applier.DamagePerSecond = damagePerSecond;
        }

        public override void RemoveFrom(GameObject obj)
        {
            if (obj.TryGetComponent(out StandStillDamageApplier applier))
            {
                Destroy(applier);
            }
        }
    }
}