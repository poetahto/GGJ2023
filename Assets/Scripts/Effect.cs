using UnityEngine;

namespace Effects
{
    public abstract class Effect : MonoBehaviour
    {
        public abstract string GetName();
        public abstract string GetDescription();
        public abstract void ApplyTo(GameObject obj); 
        public abstract void RemoveFrom(GameObject obj);
    }
}