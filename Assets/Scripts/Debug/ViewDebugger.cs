using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class ViewDebugger<T> : MonoBehaviour where T : Component 
{
    [SerializeField] private View<T> prefab;
    [SerializeField] private Vector3 offset;
    
    private ObjectPool<View<T>> _healthViewPool;
    private Stack<View<T>> _usedHealthViews;

    private void Awake()
    {
        _healthViewPool = new ObjectPool<View<T>>(() => Instantiate(prefab, transform));
        _usedHealthViews = new Stack<View<T>>();
    }

    private void Update()
    {
        _usedHealthViews.Clear();
        
        foreach (var health in FindObjectsByType<T>(FindObjectsSortMode.None))
        {
            var healthView = _healthViewPool.Get();
            healthView.UpdateValue(health);
            healthView.transform.position = health.transform.position + offset;
            _usedHealthViews.Push(healthView);
        }

        foreach (var usedHealthView in _usedHealthViews)
        {
            _healthViewPool.Release(usedHealthView);
        }
    }
}