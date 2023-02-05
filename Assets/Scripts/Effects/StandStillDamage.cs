using UnityEngine;

namespace Effects
{
    public class StandStillDamage : Effect
    {
        private readonly float _minSpeed;
        private readonly float _damagePerSecond;
        private float _damageCooldown;
        private Rigidbody _rigidbody;
        private Health _health;

        public override string Name => "Abhor";

        public override string Description => "Standing still deals damage.";

        public StandStillDamage(float minSpeed, float damagePerSecond)
        {
            _minSpeed = minSpeed;
            _damagePerSecond = damagePerSecond;
        }
        
        public override void Initialize()
        {
            _health = Player.GetComponent<Health>();
            _rigidbody = Player.GetComponent<Rigidbody>();
        }

        public override void Update()
        {
            if (_rigidbody.velocity.sqrMagnitude < _minSpeed * _minSpeed)
            {
                if (_damageCooldown <= 0)
                {
                    _health.Damage(1);
                    _damageCooldown = 1 / _damagePerSecond;
                }
                else _damageCooldown -= Time.deltaTime;
            }
        }

        public override void Shutdown()
        {
        }
    }
}