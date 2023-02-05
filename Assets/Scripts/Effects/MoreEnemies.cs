namespace Effects
{
    public class MoreEnemies : Effect
    {
        public override string Name        => "Deluge";
        public override string Description => "More enemies arrive to attack you.";

        private readonly int _amount;
        
        public MoreEnemies(int amount)
        {
            _amount = amount;
        }
        
        public override void Initialize()
        {
            CombatManager.instance.maxEnemyCount += _amount;
            CombatManager.instance.maxEnemiesPerWave += _amount;
        }

        public override void Shutdown()
        {
            CombatManager.instance.maxEnemyCount -= _amount;
            CombatManager.instance.maxEnemiesPerWave -= _amount;
        }
    }
}