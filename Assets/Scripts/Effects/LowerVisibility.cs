using DefaultNamespace.VFX;
using UnityEngine;

namespace Effects
{
    public class LowerVisibility : Effect
    {
        private readonly float _amount;
        
        public override string Name => "Envelop";
        
        public override string Description => "Lowers visibility.";

        public LowerVisibility(float amount)
        {
            _amount = amount;
        }

        public override void Initialize()
        {
            var blindness = Object.FindFirstObjectByType<BlindnessVFX>();
            blindness.AddBlindness(_amount);
        }

        public override void Shutdown()
        {
            var blindness = Object.FindFirstObjectByType<BlindnessVFX>();
            blindness.RemoveBlindness(_amount);
        }
    }
}