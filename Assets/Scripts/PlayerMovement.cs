using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Serializable]
    public struct MovementSettings
    {
        public float speed;
        public float acceleration;
        public float deceleration;
        public float reverseMultiplier;
    }

    [SerializeField] private float decelerationAmount = 5;
    [SerializeField] private MovementSettings groundedMovement;

    private Rigidbody _rigidbody;
    private bool _hasGroundChecker;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move(groundedMovement);
    }

    private Vector2 GetInputDirection()
    {
        return new Vector2(
            // Input.GetAxisRaw("Horizontal"),
            // Input.GetAxisRaw("Vertical")
            GetAxis(KeyCode.D, KeyCode.A),
            GetAxis(KeyCode.W, KeyCode.S)
        );
    }

    private static float GetAxis(KeyCode positive, KeyCode negative)
    {
        float result = 0;
        if (Input.GetKey(positive)) result++;
        if (Input.GetKey(negative)) result--;
        return result;
    }

    private void Move(MovementSettings settings)
    {
        Vector3 inputDirection = GetInputDirection();
        Vector3 currentVelocity = _rigidbody.velocity;

        var targetVelocity = inputDirection.normalized * settings.speed;
        
        bool didReverseX = 
            targetVelocity.x != 0 && 
            currentVelocity.x != 0 &&
            (int) Mathf.Sign(targetVelocity.x) != (int) Mathf.Sign(currentVelocity.x);
        
        bool didReverseY = 
            targetVelocity.z != 0 && 
            currentVelocity.z != 0 &&
            (int) Mathf.Sign(targetVelocity.z) != (int) Mathf.Sign(currentVelocity.z);
        
        float acceleration = inputDirection != Vector3.zero 
            ? settings.acceleration 
            : settings.deceleration;

        if (inputDirection != Vector3.zero)
        {
            var t1 = (int) Mathf.Sign(targetVelocity.x) == (int) Mathf.Sign(currentVelocity.x) && currentVelocity.magnitude > targetVelocity.magnitude;
            var t2 = (int) Mathf.Sign(targetVelocity.z) == (int) Mathf.Sign(currentVelocity.z) && currentVelocity.magnitude > targetVelocity.magnitude;
            if (t1 || t2)
                return;
            
            // is accelerating
            float reverseMultiplier = didReverseX || didReverseY ? settings.reverseMultiplier : 1;
            float maxDelta = acceleration * reverseMultiplier * Time.deltaTime;
            _rigidbody.velocity = Vector3.MoveTowards(currentVelocity, targetVelocity, maxDelta);
        }
        else
        {
            // is decelerating
            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, Vector3.zero, decelerationAmount * Time.deltaTime);
        }
    }
}