using UnityEngine;

namespace BulletModifications
{
    public class ApplyDamageOnCollision : BulletModifier
    {
        private readonly float _amount;

        public ApplyDamageOnCollision(float amount)
        {
            _amount = amount;
        }

        public override BulletModifier Copy()
        {
            return new ApplyDamageOnCollision(_amount);
        }

        public override void OnHit(Collider col)
        {
            if (col.TryGetComponent(out Health health))
            {
                health.Damage(_amount);
            }
        }
    }
}