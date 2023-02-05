using UnityEngine;

namespace Effects
{
    public class ApplyDamageOnCollision : CollisionEffect
    {
        public float amount;
        
        public override void HandleCollisionEnter(Collider col, GameObject source)
        {
            if (col.TryGetComponent(out Health health))
            {
                health.Damage(amount);
            }
        }

        public override string GetName()
        {
            return string.Empty;
        }

        public override string GetDescription()
        {
            return string.Empty;
        }
    }
}