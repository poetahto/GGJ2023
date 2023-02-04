using Events;
using UnityEngine;

namespace Effects
{
    public abstract class CollisionEffect : Effect
    {
        public override void ApplyTo(GameObject obj)
        {
            if (obj.TryGetComponent(out Collider col))
            {
                var triggerEvents = col.GetTriggerEvents();
                triggerEvents.ColliderTriggerEnter += o => HandleCollisionEnter(o, obj);
                triggerEvents.ColliderTriggerExit += o => HandleCollisionExit(o, obj);
            }
        }

        public override void RemoveFrom(GameObject obj)
        {
        }
        
        public virtual void HandleCollisionEnter(Collider col, GameObject source) {}
        public virtual void HandleCollisionExit(Collider col, GameObject source) {}
    }
}