using Application.Core;
using UnityEngine;

namespace Effects
{
    public class IgnoreLayerEffect : Effect
    {
        public LayerMask mask;
        
        public override void ApplyTo(GameObject obj)
        {
            if (obj.TryGetComponent(out Collider col))
            {
                var te = col.GetTriggerEvents();
                te.ExcludeLayers = mask;
            }
        }

        public override void RemoveFrom(GameObject obj)
        {
        }
    }
}