using BulletModifications;
using UnityEngine;

namespace Effects
{
    public class ExplodeOnImpact : Effect
    {
        public override string Name        => "Erupt";
        public override string Description => "Bullets explode on impact.";

        private readonly AoeDamageOnCollision _aoeModifier;
        private readonly SpawnOnCollision _spawnModifier;
        
        public ExplodeOnImpact(float radius, float damage, GameObject spawnOnExplode)
        {
            _aoeModifier = new AoeDamageOnCollision(damage, radius);
            _spawnModifier = new SpawnOnCollision(spawnOnExplode);
        }
        
        public override void Initialize()
        {
            if (Player.TryGetComponent(out BulletSpawner spawner))
            {
                spawner.bulletModifiers.Add(_aoeModifier);
                spawner.bulletModifiers.Add(_spawnModifier);
            }
        }

        public override void Shutdown()
        {
            if (Player.TryGetComponent(out BulletSpawner spawner))
            {
                spawner.bulletModifiers.Remove(_aoeModifier);
                spawner.bulletModifiers.Remove(_spawnModifier);
            }
        }
    }
}