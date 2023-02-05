using UnityEngine;

namespace BulletModifications
{
    public class DestroyOnCollision : BulletModifier
    {
        public override void OnHit(Collider col)
        {
            Object.Destroy(Bullet);
        }
    }
}