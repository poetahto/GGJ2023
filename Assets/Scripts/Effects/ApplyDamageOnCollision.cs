using UnityEngine;

namespace Effects
{
    public class ApplyDamageOnCollision : CollisionEffect
    {
        public float amount;
        
        public override void HandleCollisionEnter(Collider2D col, GameObject source)
        {
            if (col.TryGetComponent(out Health health))
            {
                health.Damage(amount);
            }
        }
    }
}