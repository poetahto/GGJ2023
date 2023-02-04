using UnityEngine;

public class MaxHealthBoost : Effect
{
    public override void ApplyTo(GameObject obj)
    {
        if (obj.TryGetComponent(out Health health))
        {
            health.maxValue += 10;
        }
    }

    public override void RemoveFrom(GameObject obj)
    {
        if (obj.TryGetComponent(out Health health))
        {
            health.maxValue -= 10;
        }
    }
}
