using System.Collections.Generic;
using Application.Core;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    // todo: all of these need to be able to change for powerup reasons
    [SerializeField] private GameObject bulletPrefab;

    public List<Effect> bulletEffects;
    public float bulletSpeed;
    public float fireRate;

    private float _cooldownTime;
    
    public Vector2 FiringDirection { get; set; }
    public bool IsFiring { get; set; }

    private void Update()
    {
        if (IsFiring && _cooldownTime <= 0)
        {
            // todo: pool
            var instance = Instantiate(bulletPrefab);
            
            foreach (var effect in bulletEffects)
            {
                effect.ApplyTo(instance);
            }
            
            instance.transform.position = transform.position;
            _cooldownTime = 1 / fireRate;

            if (instance.TryGetComponent(out Rigidbody2D rb))
            {
                rb.velocity = FiringDirection * bulletSpeed;
            }
        }
        else _cooldownTime -= Time.deltaTime;
    }
}