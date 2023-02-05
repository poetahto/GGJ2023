namespace Effects
{
    public class TakeMoreDamage : Effect
    {
        private readonly float _extraDamageAmount = 0.5f;

        public override string Name => "Decrepit";

        public override string Description => "You receive more damage.";

        public TakeMoreDamage(float extraDamage)
        {
            _extraDamageAmount = extraDamage;
        }
        
        public override void Initialize()
        {
            if (Player.TryGetComponent(out Health health))
            {
                health.onDamage.AddListener(ApplyExtraDamage);
            }
        }

        public override void Shutdown()
        {
            if (Player.TryGetComponent(out Health health))
            {
                health.onDamage.RemoveListener(ApplyExtraDamage);
            }
        }

        private void ApplyExtraDamage(HealthDamageEvent data)
        {
            data.Health.SetHealth(data.Health.CurrentHealth - _extraDamageAmount);
        }
    }
}