namespace Effects
{
    public class LowFrictionEffect : Effect
    {
        private readonly float _amount;
        
        public override string Name => "Sever";

        public override string Description => "You have less friction with the ground.";

        public LowFrictionEffect(float amount)
        {
            _amount = amount;
        }
        
        public override void Initialize()
        {
            if (Player.TryGetComponent(out PlayerMovement movement))
            {
                movement.decelerationAmount += _amount;
            }
        }

        public override void Shutdown()
        {
            if (Player.TryGetComponent(out PlayerMovement movement))
            {
                movement.decelerationAmount -= _amount;
            }
        }
    }
}