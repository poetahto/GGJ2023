using UnityEngine;

namespace Effects
{
    public class ChildrenEffects : Effect
    {
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
            foreach (var effect in GetComponentsInChildren<Effect>())
            {
                if (effect != this)
                    effect.ApplyTo(obj);
            }
        }

        public override void RemoveFrom(GameObject obj)
        {
            foreach (var effect in GetComponentsInChildren<Effect>())
            {
                if (effect != this)
                    effect.RemoveFrom(obj);
            }
        }
    }
}