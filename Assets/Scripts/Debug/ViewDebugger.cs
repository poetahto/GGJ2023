using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Debug
{
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
            foreach (var usedHealthView in _usedHealthViews)
            {
                usedHealthView.gameObject.SetActive(false);
                _healthViewPool.Release(usedHealthView);
            }
        
            _usedHealthViews.Clear();
        
            foreach (var health in FindObjectsByType<T>(FindObjectsSortMode.None))
            {
                var healthView = _healthViewPool.Get();
                healthView.gameObject.SetActive(true);
                healthView.UpdateValue(health);
                healthView.transform.position = health.transform.position + offset;
                _usedHealthViews.Push(healthView);
            }

        
        }
    }
}