using UnityEngine;

namespace Effects
{
    public class HigherBulletDamage : Effect
    {
        public ApplyDamageOnCollision extraDamageComponent;

        public override string GetName() => "Wrath";

        public override string GetDescription() => "Your attacks deal more damage.";

        public override void ApplyTo(GameObject obj)
        {
            if (obj.TryGetComponent(out BulletSpawner spawner))
            {
                spawner.bulletEffects.Add(extraDamageComponent);
            }
        }

        public override void RemoveFrom(GameObject obj) {}
    }
}