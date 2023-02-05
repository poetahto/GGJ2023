using UnityEngine;

namespace BulletModifications
{
    public class VampiricModifier : BulletModifier
    {
        private readonly Health _target;
        private readonly float _amount;
        
        public VampiricModifier(Health target, float amount)
        {
            _target = target;
            _amount = amount;
        }
        
        public override void OnHit(Collider col)
        {
            _target.Heal(_amount);
        }

        public override BulletModifier Copy()
        {
            return new VampiricModifier(_target, _amount);
        }
    }
}