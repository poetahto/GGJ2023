using UnityEngine;
using UnityEngine.Events;

namespace Collectables
{
    public class Storable : Collectable
    {
        [SerializeField] 
        private UnityEvent<GameObject> onDrop;
    
        public void Drop(GameObject dropSource)
        {
            onDrop.Invoke(dropSource);
        }
    }
}