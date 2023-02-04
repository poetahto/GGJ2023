using System;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
    [SerializeField] private GameObject aimIndicator;
    [SerializeField] private float indicatorMult = 0.25f;
    [SerializeField] private BulletSpawner spawner;
        
    private Vector3 GetFiringDirection()
    {
        var playerPos = transform.position;
        playerPos.y = 0;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit info);
        var cursorPos = info.point;
        cursorPos.y = 0;
        var result = (cursorPos - playerPos).normalized;
        return result;
    }

    private void Update()
    {
        spawner.FiringDirection = GetFiringDirection();
        spawner.IsFiring = Input.GetKey(KeyCode.Mouse0);
            
        aimIndicator.transform.position = transform.position + spawner.FiringDirection * indicatorMult;
            
            
    }
}