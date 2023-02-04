using UnityEngine;

public class HealthRestore : Effect
{
    public override void ApplyTo(GameObject obj)
    {
        if (obj.TryGetComponent(out Health health))
        {
            health.value = health.maxValue;
        }
    }

    public override void RemoveFrom(GameObject obj)
    {
    }
}