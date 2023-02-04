using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    [SerializeField] private bool applyToSelfOnAwake;

    private void Awake()
    {
        if (applyToSelfOnAwake)
        {
            ApplyTo(gameObject);
        }
    }

    private void OnDestroy()
    {
        RemoveFrom(gameObject);
    }

    public abstract void ApplyTo(GameObject obj); 
    public abstract void RemoveFrom(GameObject obj);
}