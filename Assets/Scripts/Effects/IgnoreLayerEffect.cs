﻿using Events;
using UnityEngine;

namespace Effects
{
    public class IgnoreLayerEffect : Effect
    {
        public LayerMask mask;

        public override string GetName()
        {
            return string.Empty;
        }

        public override string GetDescription()
        {
            return string.Empty;
        }

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