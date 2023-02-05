namespace Effects
{
    public class MaxHealthBoost : Effect
    {
        private readonly float _amount;

        public override string Name => "Stature";

        public override string Description => "Your maximum health is increased.";

        public MaxHealthBoost(float amount)
        {
            _amount = amount;
        }
        
        public override void Initialize()
        {
            if (Player.TryGetComponent(out Health health))
            {
                health.MaxHealth += _amount;
            }
        }

        public override void Shutdown()
        {
            if (Player.TryGetComponent(out Health health))
            {
                health.MaxHealth -= _amount;
            }
        }
    }
}
