namespace Effects
{
    public class Slowdown : Effect
    {
        public override string Name        => "Stagnate";
        public override string Description => "You move slower.";

        private readonly float _amount;
        
        public Slowdown(float amount)
        {
            _amount = amount;
        }
        
        public override void Initialize()
        {
            if (Player.TryGetComponent(out PlayerMovement movement))
            {
                movement.groundedMovement.speed *= _amount;
            }
        }

        public override void Shutdown()
        {
            if (Player.TryGetComponent(out PlayerMovement movement))
            {
                movement.groundedMovement.speed /= _amount;
            }
        }
    }
}