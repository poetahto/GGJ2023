using Events;
using UnityEngine;

namespace BulletModifications
{
    public class IgnoreLayer : BulletModifier 
    {
        private readonly LayerMask _mask;

        public IgnoreLayer(LayerMask mask)
        {
            _mask = mask;
        }

        public override BulletModifier Copy()
        {
            return new IgnoreLayer(_mask);
        }

        public override void Initialize()
        {
            if (Bullet.TryGetComponent(out Collider col))
            {
                var te = col.GetTriggerEvents();
                te.ExcludeLayers = _mask;
            }
        }
    }
}