using UnityEngine;
using UnityEngine.Events;

namespace Collectables
{
    public class ItemCollectEvent
    {
        public GameObject item;
        public GameObject collector;
    }
    
    public class Collectable : MonoBehaviour
    {
        [SerializeField] 
        public  UnityEvent<ItemCollectEvent> onCollect;
    
        public void Collect(GameObject collectSource)
        {
            var data = new ItemCollectEvent
            {
                collector = collectSource,
                item = gameObject,
            };

            onCollect.Invoke(data);
        }
    }
}