using UnityEngine;
using UnityEngine.Events;

public class PlantHunterLogic : MonoBehaviour
{
    public UnityEvent onDestruct;

    public float deathDamage = 1;
    public float chaseSpeed = 1;
    public float selfDestructRange;
    
    private Plant _target;
    private Rigidbody _rigidbody;
    
    private void Awake()
    {
        _target = FindAnyObjectByType<Plant>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var distToTarget = _target.transform.position - transform.position;
        var dirToTarget = distToTarget.normalized;
        _rigidbody.velocity = dirToTarget * chaseSpeed;

        if (distToTarget.sqrMagnitude < selfDestructRange * selfDestructRange && _target.TryGetComponent(out Health health))
        {
            health.Damage(deathDamage);
            onDestruct.Invoke();
            Destroy(gameObject);
        }
    }
}