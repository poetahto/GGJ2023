using BulletModifications;

namespace Effects
{
    public class BiggerBullets : Effect
    {
        public override string Name        => "Impose";
        public override string Description => "Increase bullet size.";

        private readonly float _amount;
        private ScaleOnSpawn _modifier;
        
        public BiggerBullets(float amount)
        {
            _amount = amount;
        }
        
        public override void Initialize()
        {
            _modifier ??= new ScaleOnSpawn(_amount);
            
            if (Player.TryGetComponent(out BulletSpawner spawner))
            {
                spawner.bulletModifiers.Add(_modifier);
            }
        }

        public override void Shutdown()
        {
            if (Player.TryGetComponent(out BulletSpawner spawner))
            {
                spawner.bulletModifiers.Remove(_modifier);
            }
        }
    }
}