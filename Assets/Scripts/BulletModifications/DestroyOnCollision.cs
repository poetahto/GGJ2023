using UnityEngine;

namespace BulletModifications
{
    public class DestroyOnCollision : BulletModifier
    {
        public override BulletModifier Copy()
        {
            return new DestroyOnCollision();
        }

        public override void OnHit(Collider col)
        {
            Object.Destroy(Bullet, 0.1f);
        }
    }
}