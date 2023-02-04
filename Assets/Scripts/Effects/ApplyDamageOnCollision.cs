using UnityEngine;

namespace Effects
{
    public class ApplyDamageOnCollision : CollisionEffect
    {
        public override void HandleCollisionEnter(Collider2D col, GameObject source)
        {
            if (col.TryGetComponent(out Health health))
            {
                health.value -= 1;
            }
        }
    }
}