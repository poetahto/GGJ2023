﻿using UnityEngine;

public class HealthRestore : Effect
{
    public float amount;
    public bool fullHeal;
    
    public override void ApplyTo(GameObject obj)
    {
        if (obj.TryGetComponent(out Health health))
        {
            health.Heal(fullHeal ? health.MaxHealth : amount);
        }
    }

    public override void RemoveFrom(GameObject obj)
    {
    }
}