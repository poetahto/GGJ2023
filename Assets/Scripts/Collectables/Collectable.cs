using UnityEngine;
using UnityEngine.Events;

namespace Collectables
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] 
        public  UnityEvent<GameObject> onCollect;
    
        public void Collect(GameObject collectSource)
        {
            onCollect.Invoke(collectSource);
        }
    }
}