namespace Effects
{
    public class LongerRound : Effect
    {
        public override string Name => "Dread";
        public override string Description => "Waves of enemies attack for longer.";

        private readonly float _amount;
        
        public LongerRound(float amount)
        {
            _amount = amount;
        }

        public override void Initialize()
        {
            CombatManager.instance.duration += _amount;
        }

        public override void Shutdown()
        {
            CombatManager.instance.duration -= _amount;
        }
    }
}