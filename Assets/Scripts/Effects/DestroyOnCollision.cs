using UnityEngine;

namespace Effects
{
    public class DestroyOnCollision : CollisionEffect
    {
        public override void HandleCollisionEnter(Collider2D col, GameObject source)
        {
            Destroy(source);
        }
    }
}