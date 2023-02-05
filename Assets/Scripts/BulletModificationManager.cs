using System.Collections.Generic;
using Events;
using UnityEngine;

[RequireComponent(typeof(TriggerEvents))]
public class BulletModificationManager : MonoBehaviour
{
    private List<BulletModifier> _modifiers = new List<BulletModifier>();
    private TriggerEvents _triggerEvents;

    private void Awake()
    {
        _triggerEvents = GetComponent<TriggerEvents>();
        _triggerEvents.ColliderTriggerEnter += HandleBulletHit;
    }

    private void OnDestroy()
    {
        _triggerEvents.ColliderTriggerEnter -= HandleBulletHit;
    }

    public void AddModifier(BulletModifier modifier)
    {
        modifier.Bullet = gameObject;
        modifier.Initialize();
            
        _modifiers.Add(modifier);
    }

    private void Update()
    {
        foreach (var bulletModifier in _modifiers)
        {
            bulletModifier.Update();
        }
    }

    private void HandleBulletHit(Collider other)
    {
        foreach (var bulletModifier in _modifiers)
        {
            bulletModifier.OnHit(other);
        }
    }
}