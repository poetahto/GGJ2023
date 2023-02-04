using System;
using UnityEngine;

namespace Application.Core
{
    public class PlayerAttacking : MonoBehaviour
    {
        [SerializeField] private GameObject aimIndicator;
        [SerializeField] private float indicatorMult = 0.25f;
        [SerializeField] private BulletSpawner spawner;
        
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
            spawner.FiringDirection = GetFiringDirection();
            spawner.IsFiring = Input.GetKey(KeyCode.Mouse0);
            
            aimIndicator.transform.position = transform.position + (Vector3) spawner.FiringDirection * indicatorMult;
            
            
        }
    }
}