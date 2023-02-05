using System.Collections.Generic;
using Effects;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletSpawner : MonoBehaviour
{
    // todo: all of these need to be able to change for powerup reasons
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject intialEffects;
    [SerializeField] private Transform spawnLocation;

    public List<Effect> bulletEffects;
    public float bulletSpeed;
    public float fireRate;
    public float spread = 10f;

    public ScreenShaker shaker;
    private float _cooldownTime;
    
    public Vector3 FiringDirection { get; set; }
    public bool IsFiring { get; set; }

    private void Awake()
    {
        foreach (var effect in intialEffects.GetComponents<Effect>())
        {
            bulletEffects.Add(effect);
        }
    }

    private void Update()
    {
        
        if (IsFiring && _cooldownTime <= 0)
        {
            // todo: pool
            var instance = Instantiate(bulletPrefab);
            if(shaker)
            shaker.ShootShake();
            foreach (var effect in bulletEffects)
            {
                effect.ApplyTo(instance);
            }
            
            instance.transform.position = spawnLocation.position;
            _cooldownTime = 1 / fireRate;

            if (instance.TryGetComponent(out Rigidbody rb))
            {
                float randomSpreadAngle = Random.Range(-spread, spread);
                Quaternion randomRotation = Quaternion.Euler(0, randomSpreadAngle, 0);
                rb.velocity = (randomRotation * FiringDirection) * bulletSpeed;
            }
        }
        else _cooldownTime -= Time.deltaTime;
    }
}