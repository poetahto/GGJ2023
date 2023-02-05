using UnityEngine;

namespace Effects
{
    public class LowFrictionEffect : Effect
    {
        public float amount;
        
        public override string GetName() => "Sever";

        public override string GetDescription() => "You have less friction with the ground.";

        public override void ApplyTo(GameObject obj)
        {
            if (obj.TryGetComponent(out PlayerMovement movement))
            {
                movement.decelerationAmount += amount;
            }
        }

        public override void RemoveFrom(GameObject obj)
        {
            if (obj.TryGetComponent(out PlayerMovement movement))
            {
                movement.decelerationAmount -= amount;
            }
        }
    }
}