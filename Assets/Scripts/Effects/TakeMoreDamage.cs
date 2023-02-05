﻿using UnityEngine;

namespace Effects
{
    public class TakeMoreDamage : Effect
    {
        public float extraDamageAmount = 0.5f;

        public override string GetName() => "Decrepit";

        public override string GetDescription() => "You receive more damage.";

        public override void ApplyTo(GameObject obj)
        {
            if (obj.TryGetComponent(out Health health))
            {
                health.onDamage.AddListener(_ =>
                {
                    health.SetHealth(health.CurrentHealth - extraDamageAmount);
                });
            }
        }

        public override void RemoveFrom(GameObject obj) {}
    }
}