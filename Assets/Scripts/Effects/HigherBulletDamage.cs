using UnityEngine;

namespace Effects
{
    public class HigherBulletDamage : Effect
    {
        public ApplyDamageOnCollision extraDamageComponent;
        
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