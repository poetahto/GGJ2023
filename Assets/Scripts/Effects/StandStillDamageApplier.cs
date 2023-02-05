using UnityEngine;

namespace Effects
{
    public class StandStillDamageApplier : MonoBehaviour
    {
        public float MinSpeed { get; set; }
        public float DamagePerSecond { get; set; }
        
        private Health _health;
        private Rigidbody _rigidbody;
        private float _damageCooldown = 0;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_rigidbody.velocity.sqrMagnitude < MinSpeed * MinSpeed)
            {
                if (_damageCooldown <= 0)
                {
                    _health.Damage(1);
                    _damageCooldown = 1 / DamagePerSecond;
                }
                else _damageCooldown -= Time.deltaTime;
            }
        }
    }
}