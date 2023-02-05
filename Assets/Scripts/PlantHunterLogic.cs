using System.Collections;
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

    public float spawnDelay;
    public float spawnDistance;
    public bool spawning = true;
    private void Awake()
    {
        _target = FindAnyObjectByType<Plant>();
        _rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(spawnProcess());
    }
    IEnumerator spawnProcess()
    {
        spawning = true;
        Vector3 endPos = transform.position;
        Vector3 startPos = transform.position + Vector3.down * spawnDistance;
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
        var distToTarget = _target.transform.position - transform.position;
        var dirToTarget = distToTarget.normalized;
        _rigidbody.velocity = dirToTarget * chaseSpeed;

        if (distToTarget.sqrMagnitude < selfDestructRange * selfDestructRange)
        {
            if (_target.TryGetComponent(out Health health))
            {
                health.Damage(deathDamage);
            }
            
            onDestruct.Invoke();
            Destroy(gameObject);
        }
    }
}