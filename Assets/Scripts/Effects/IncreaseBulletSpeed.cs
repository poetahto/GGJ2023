namespace Effects
{
    public class IncreaseBulletSpeed : Effect
    {
        public override string Name        => "Rage";
        public override string Description => "Increases bullet speed.";

        private readonly float _amount;
        
        public IncreaseBulletSpeed(float amount)
        {
            _amount = amount;
        }
        
        public override void Initialize()
        {
            if (Player.TryGetComponent(out BulletSpawner spawner))
            {
                spawner.bulletSpeed += _amount;
            }
        }

        public override void Shutdown()
        {
            if (Player.TryGetComponent(out BulletSpawner spawner))
            {
                spawner.bulletSpeed -= _amount;
            }
        }
    }
}