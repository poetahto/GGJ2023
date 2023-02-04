using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Collectables
{
    public class Storage : MonoBehaviour
    {
        [SerializeField] 
        private UnityEvent<Storable> onStore;
    
        private List<Storable> _storables = new List<Storable>();

        public IEnumerable<Storable> Storable => _storables;
    
        public void Store(Collectable collectable)
        {
            if (collectable is Storable storable)
            {
                Store(storable);
            }
        }

        public void Store(Storable storable)
        {
            _storables.Add(storable);
            onStore.Invoke(storable);
        }
    }
}