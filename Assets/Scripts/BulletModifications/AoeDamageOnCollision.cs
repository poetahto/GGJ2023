using Events;
using UnityEngine;

namespace BulletModifications
{
    public class AoeDamageOnCollision : BulletModifier
    {
        private readonly float _damage;
        private readonly float _radius;
        
        public AoeDamageOnCollision(float damage, float radius)
        {
            _damage = damage;
            _radius = radius;
        }

        public override void OnHit(Collider col)
        {
            if (Bullet.TryGetComponent(out TriggerEvents te))
            {
                foreach (var obj in Physics.OverlapSphere(Bullet.transform.position, _radius, ~te.ExcludeLayers))
                {
                    if (obj.TryGetComponent(out Health health))
                    {
                        health.Damage(_damage);
                    }
                }   
            }
        }

        public override BulletModifier Copy()
        {
            return new AoeDamageOnCollision(_damage, _radius);
        }
    }
}