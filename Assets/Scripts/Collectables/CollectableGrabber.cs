using UnityEngine;
using UnityEngine.Events;

public class CollectableGrabber : MonoBehaviour
{
    [SerializeField] 
    private UnityEvent<Collectable> onCollect;

    [SerializeField] 
    private GameObject source;

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out Collectable collectable))
        {
            collectable.Collect(source);
            onCollect.Invoke(collectable);
        }
    }
}