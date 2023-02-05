using BulletModifications;

namespace Effects
{
    public class HigherBulletDamage : Effect
    {
        private readonly ApplyDamageOnCollision _extraDamage;
        
        public override string Name => "Wrath";

        public override string Description => "Your attacks deal more damage.";

        public HigherBulletDamage(float extraDamage)
        {
            _extraDamage = new ApplyDamageOnCollision(extraDamage);
        }
        
        public override void Initialize()
        {
            if (Player.TryGetComponent(out BulletSpawner spawner))
            {
                spawner.bulletModifiers.Add(_extraDamage);
            }
        }

        public override void Shutdown()
        {
            if (Player.TryGetComponent(out BulletSpawner spawner))
            {
                spawner.bulletModifiers.Remove(_extraDamage);
            }
        }
    }
}