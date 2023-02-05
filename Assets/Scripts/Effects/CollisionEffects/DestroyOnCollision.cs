using UnityEngine;

namespace Effects
{
    public class DestroyOnCollision : CollisionEffect
    {
        public override void HandleCollisionEnter(Collider col, GameObject source)
        {
            Destroy(source);
        }
    }
}