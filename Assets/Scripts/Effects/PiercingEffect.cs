using BulletModifications;

namespace Effects
{
    public class PiercingEffect : Effect
    {
        public override string Name        => "Gore";
        public override string Description => "Your bullets pierce through enemies.";

        public override void Initialize()
        {
            if (Player.TryGetComponent(out BulletSpawner spawner))
            {
                for (int i = spawner.bulletModifiers.Count - 1; i >= 0; i--)
                {
                    if (spawner.bulletModifiers[i] is DestroyOnCollision dot)
                    {
                        spawner.bulletModifiers.Remove(dot);
                    }
                }
            }
        }

        public override void Shutdown()
        {
            if (Player.TryGetComponent(out BulletSpawner spawner))
            {
                spawner.bulletModifiers.Add(new DestroyOnCollision());
            }
        }
    }
}