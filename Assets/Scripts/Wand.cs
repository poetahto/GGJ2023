using System.Collections.Generic;
using Application.Core;
using UnityEngine;

public class Wand : MonoBehaviour
{
    // todo: all of these need to be able to change for powerup reasons
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject aimIndicator;
    [SerializeField] private float indicatorMult = 0.25f;

    public List<Effect> bulletEffects;
    public float bulletSpeed;
    public float fireRate;

    private float _cooldownTime;

    private Vector2 GetFiringDirection()
    {
        var playerPos = transform.position;
        var cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = 0;
        var result = (cursorPos - playerPos).normalized;
        return result;
    }
    
    private void Update()
    {
        aimIndicator.transform.position = transform.position + (Vector3) GetFiringDirection() * indicatorMult;

        if (Input.GetKey(KeyCode.Mouse0) && _cooldownTime <= 0)
        {
            // todo: pool
            var instance = Instantiate(bulletPrefab);

            if (instance.TryGetComponent(out Collider2D col))
            {
                var te = col.GetTriggerEvents();
                te.ExcludeLayers = LayerMask.GetMask("Player");
            }
            
            foreach (var effect in bulletEffects)
            {
                effect.ApplyTo(instance);
            }
            
            instance.transform.position = aimIndicator.transform.position;
            _cooldownTime = 1 / fireRate;

            if (instance.TryGetComponent(out Rigidbody2D rb))
            {
                rb.velocity = GetFiringDirection() * bulletSpeed;
            }
        }
        else _cooldownTime -= Time.deltaTime;
    }
}