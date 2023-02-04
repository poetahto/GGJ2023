﻿using System;
using UnityEngine;

namespace Application.Core
{
    public class PlayerAttacking : MonoBehaviour
    {
        [SerializeField] private GameObject aimIndicator;
        [SerializeField] private float indicatorMult = 0.25f;
        
        private BulletSpawner _spawner;

        private void Awake()
        {
            _spawner = GetComponent<BulletSpawner>();
        }
        
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
            _spawner.FiringDirection = GetFiringDirection();
            _spawner.IsFiring = Input.GetKey(KeyCode.Mouse0);
            
            aimIndicator.transform.position = transform.position + (Vector3) _spawner.FiringDirection * indicatorMult;
            
            
        }
    }
}