using System.Collections;
using UnityEngine;

public class PlayerHunterLogic : MonoBehaviour
{
    public float chaseSpeed = 1;
    public float firingSpeed = 0.5f;
    public float attackRange = 8;
        
    private Transform _target;
    private Rigidbody _rigidbody;
    private BulletSpawner _spawner;
    public float spawnDelay;
    public float spawnDistance;
    public bool spawning = true;
    private void Awake()
    {
        _target = FindAnyObjectByType<PlayerMovement>().transform;
        _rigidbody = GetComponent<Rigidbody>();
        _spawner = GetComponent<BulletSpawner>();
        StartCoroutine(spawnProcess());
    }
    IEnumerator spawnProcess()
    {
        spawning = true;
        Vector3 endPos = transform.position;
        Vector3 startPos = transform.position +Vector3.down*spawnDistance;
        for (float t = 0f; t < spawnDelay; t += Time.deltaTime)
        {
            float normalizedTime = t / spawnDelay;
            //right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
            transform.position = Vector3.Lerp(startPos, endPos, normalizedTime);
            yield return null;
        }
        spawning = false;
    }
    private void FixedUpdate()
    {
        if (spawning)
        {
            return;
        }
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