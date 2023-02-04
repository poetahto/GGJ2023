using UnityEngine;

public class PlayerHunterLogic : MonoBehaviour
{
    public float chaseSpeed = 1;
    public float firingSpeed = 0.5f;
    public float attackRange = 8;
        
    private Transform _target;
    private Rigidbody _rigidbody;
    private BulletSpawner _spawner;
        
    private void Awake()
    {
        _target = FindAnyObjectByType<PlayerMovement>().transform;
        _rigidbody = GetComponent<Rigidbody>();
        _spawner = GetComponent<BulletSpawner>();
    }

    private void FixedUpdate()
    {
        var distToTarget = _target.position - transform.position;
        var dirToTarget = distToTarget.normalized;
        _spawner.FiringDirection = dirToTarget;

        if (distToTarget.sqrMagnitude <= attackRange * attackRange)
        {
            // attack
            _spawner.IsFiring = true;
            _rigidbody.velocity = dirToTarget * firingSpeed;
        }
        else
        {
            _spawner.IsFiring = false;
            _rigidbody.velocity = dirToTarget * chaseSpeed;
        }
    }
}