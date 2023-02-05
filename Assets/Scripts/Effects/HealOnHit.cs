using BulletModifications;

namespace Effects
{
    public class HealOnHit : Effect
    {
        public override string Name        => "Thirst";
        public override string Description => "Restore health when dealing damage to enemies.";

        private readonly float _amount;
        private VampiricModifier _modifier;
        
        public HealOnHit(float amount)
        {
            _amount = amount;
        }
        
        public override void Initialize()
        {
            _modifier ??= new VampiricModifier(Player.GetComponent<Health>(), _amount);

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