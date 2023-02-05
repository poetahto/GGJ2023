namespace Effects
{
    public class IncreaseFireRate : Effect
    {
        public override string Name        => "Haste";
        public override string Description => "Increase fire rate.";

        private readonly float _amount;
        
        public IncreaseFireRate(float amount)
        {
            _amount = amount;
        }
        
        public override void Initialize()
        {
            if (Player.TryGetComponent(out BulletSpawner spawner))
            {
                spawner.fireRate += _amount;
            }
        }

        public override void Shutdown()
        {
            if (Player.TryGetComponent(out BulletSpawner spawner))
            {
                spawner.fireRate -= _amount;
            }
        }
    }
}