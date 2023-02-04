using UnityEngine;
using UnityEngine.Events;

public class Storable : Collectable
{
    [SerializeField] 
    private UnityEvent<GameObject> onDrop;
    
    public void Drop(GameObject dropSource)
    {
        onDrop.Invoke(dropSource);
    }
}