using System;
using System.Collections.Generic;
using BulletModifications;
using Effects;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BulletSpawner : MonoBehaviour
{
    // todo: all of these need to be able to change for powerup reasons
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject intialEffects;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] public UnityEvent onShoot;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float baseDamage = 1;

    public List<BulletModifier> bulletModifiers; 
    public float bulletSpeed;
    public float fireRate;
    public float spread = 10f;

    public ScreenShaker shaker;
    private float _cooldownTime;
    
    public Vector3 FiringDirection { get; set; }
    public bool IsFiring { get; set; }

    private void Awake()
    {
        bulletModifiers = new List<BulletModifier>()
        {
            new IgnoreLayer(mask),
            new DestroyOnCollision(),
            new ApplyDamageOnCollision(baseDamage)
        };
    }

    private void Update()
    {
        if (IsFiring && _cooldownTime <= 0)
        {
            // todo: pool
            var instance = Instantiate(bulletPrefab);
            if(shaker)
                shaker.ShootShake();

            if (instance.TryGetComponent(out BulletModificationManager modificationManager))
            {
                foreach (var bulletModifier in bulletModifiers)
                {
                    modificationManager.AddModifier(bulletModifier);
                }
            }
            
            instance.transform.position = spawnLocation.position;
            _cooldownTime = 1 / fireRate;

            var angle = -Mathf.Atan2(FiringDirection.x, FiringDirection.z) * Mathf.Rad2Deg;
            instance.transform.rotation = Quaternion.Euler(0, 0, angle);

            if (instance.TryGetComponent(out Rigidbody rb))
            {
                float randomSpreadAngle = Random.Range(-spread, spread);
                Quaternion randomRotation = Quaternion.Euler(0, randomSpreadAngle, 0);
                rb.velocity = (randomRotation * FiringDirection) * bulletSpeed;
            }
            onShoot.Invoke();
        }
        else _cooldownTime -= Time.deltaTime;
    }

    private void OnGUI()
    {
            var angle = Mathf.Atan2(FiringDirection.x, FiringDirection.z) * Mathf.Rad2Deg;
        GUILayout.Label(angle.ToString());
    }
}