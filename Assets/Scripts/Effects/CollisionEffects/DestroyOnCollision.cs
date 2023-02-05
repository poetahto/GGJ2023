using UnityEngine;

namespace Effects
{
    public class DestroyOnCollision : CollisionEffect
    {
        public override void HandleCollisionEnter(Collider col, GameObject source)
        {
            Destroy(source);
        }

        public override string GetName()
        {
            return string.Empty;
        }

        public override string GetDescription()
        {
            return string.Empty;
        }
    }
}