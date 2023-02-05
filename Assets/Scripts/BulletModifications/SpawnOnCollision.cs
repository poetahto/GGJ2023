using UnityEngine;

namespace BulletModifications
{
    public class SpawnOnCollision : BulletModifier
    {
        private readonly GameObject _particlePrefab;
        
        public SpawnOnCollision(GameObject particlePrefab)
        {
            _particlePrefab = particlePrefab;
        }

        public override BulletModifier Copy()
        {
            return new SpawnOnCollision(_particlePrefab);
        }

        public override void OnHit(Collider col)
        {
            Object.Instantiate(_particlePrefab, Bullet.transform.position, Quaternion.identity);
        }
    }
}