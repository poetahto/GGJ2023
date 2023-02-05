using System.Collections.Generic;
using BulletModifications;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BulletSpawner : MonoBehaviour
{
    // todo: all of these need to be able to change for powerup reasons
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] public UnityEvent onShoot;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float baseDamage = 1;
    [SerializeField] private GameObject collisionParticles;
    [SerializeField] private float anglesPerBullet = 5;

    public List<BulletModifier> bulletModifiers; 
    public float bulletSpeed;
    public float fireRate;
    public float spread = 15f;
    public int bulletsPerShot = 1;

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
            new ApplyDamageOnCollision(baseDamage),
            new SpawnOnCollision(collisionParticles),
        };
    }

    private void Update()
    {
        if (IsFiring && _cooldownTime <= 0)
        {
            float startAngle = -(anglesPerBullet * bulletsPerShot / 2);
            for (int i = 0; i < bulletsPerShot; i++)
            {
                FireBullet(startAngle + i * anglesPerBullet);
            }
            _cooldownTime = 1 / fireRate;
        }
        else _cooldownTime -= Time.deltaTime;
    }

    private void FireBullet(float a)
    {
        // todo: pool
        var instance = Instantiate(bulletPrefab);
        if(shaker)
            shaker.ShootShake();

        if (instance.TryGetComponent(out BulletModificationManager modificationManager))
        {
            foreach (var bulletModifier in bulletModifiers)
            {
                modificationManager.AddModifier(bulletModifier.Copy());
            }
        }
        
        instance.transform.position = spawnLocation.position;

        var angle = -Mathf.Atan2(FiringDirection.x, FiringDirection.z) * Mathf.Rad2Deg;
        instance.transform.rotation = Quaternion.Euler(0, 0, angle + a);

        if (instance.TryGetComponent(out Rigidbody rb))
        {
            float randomSpreadAngle = Random.Range(-spread, spread);
            Quaternion randomRotation = Quaternion.Euler(0, randomSpreadAngle + a, 0);
            rb.velocity = (randomRotation * FiringDirection) * bulletSpeed;
        }
        onShoot.Invoke();
    }
}