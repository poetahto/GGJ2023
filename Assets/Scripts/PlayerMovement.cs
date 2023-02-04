using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
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

    private Rigidbody2D _rigidbody;
    private bool _hasGroundChecker;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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
        Vector2 inputDirection = GetInputDirection();
        Vector2 currentVelocity = _rigidbody.velocity;

        var targetVelocity = inputDirection.normalized * settings.speed;
        
        bool didReverseX = 
            targetVelocity.x != 0 && 
            currentVelocity.x != 0 &&
            (int) Mathf.Sign(targetVelocity.x) != (int) Mathf.Sign(currentVelocity.x);
        
        bool didReverseY = 
            targetVelocity.y != 0 && 
            currentVelocity.y != 0 &&
            (int) Mathf.Sign(targetVelocity.y) != (int) Mathf.Sign(currentVelocity.y);
        
        float acceleration = inputDirection != Vector2.zero 
            ? settings.acceleration 
            : settings.deceleration;

        if (inputDirection != Vector2.zero)
        {
            var t1 = (int) Mathf.Sign(targetVelocity.x) == (int) Mathf.Sign(currentVelocity.x) && currentVelocity.magnitude > targetVelocity.magnitude;
            var t2 = (int) Mathf.Sign(targetVelocity.y) == (int) Mathf.Sign(currentVelocity.y) && currentVelocity.magnitude > targetVelocity.magnitude;
            if (t1 || t2)
                return;
            
            // is accelerating
            float reverseMultiplier = didReverseX || didReverseY ? settings.reverseMultiplier : 1;
            float maxDelta = acceleration * reverseMultiplier * Time.deltaTime;
            _rigidbody.velocity = Vector2.MoveTowards(currentVelocity, targetVelocity, maxDelta);
        }
        else
        {
            // is decelerating
            _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity, Vector2.zero, decelerationAmount * Time.deltaTime);
        }
    }
}