using UnityEngine;

namespace BulletModifications
{
    public class CollisionParticleEffect : BulletModifier
    {
        private readonly GameObject _particlePrefab;
        
        public CollisionParticleEffect(GameObject particlePrefab)
        {
            _particlePrefab = particlePrefab;
        }

        public override BulletModifier Copy()
        {
            return new CollisionParticleEffect(_particlePrefab);
        }

        public override void OnHit(Collider col)
        {
            Object.Instantiate(_particlePrefab, Bullet.transform.position, Quaternion.identity);
        }
    }
}