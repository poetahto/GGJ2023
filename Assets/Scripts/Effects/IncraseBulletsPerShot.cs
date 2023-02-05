namespace Effects
{
    public class IncreaseBulletsPerShot : Effect
    {
        public override string Name        => "Amass";
        public override string Description => "You now create an addition bullet when shooting.";

        private readonly int _amount;
        
        public IncreaseBulletsPerShot(int amount)
        {
            _amount = amount;
        }
        
        public override void Initialize()
        {
            if (Player.TryGetComponent(out BulletSpawner spawner))
            {
                spawner.bulletsPerShot += _amount;
            }
        }

        public override void Shutdown()
        {
            if (Player.TryGetComponent(out BulletSpawner spawner))
            {
                spawner.bulletsPerShot -= _amount;
            }
        }
    }
}