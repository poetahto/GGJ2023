using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    [SerializeField] 
    private UnityEvent<GameObject> onCollect;
    
    public void Collect(GameObject collectSource)
    {
        onCollect.Invoke(collectSource);
    }
}