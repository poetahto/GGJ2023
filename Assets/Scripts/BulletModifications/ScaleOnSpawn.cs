using UnityEngine;

namespace BulletModifications
{
    public class ScaleOnSpawn : BulletModifier
    {
        private readonly float _amount;
        
        public ScaleOnSpawn(float amount)
        {
            _amount = amount;
        }

        public override void Initialize()
        {
            Bullet.transform.localScale += Vector3.one * _amount;
        }

        public override BulletModifier Copy()
        {
            return new ScaleOnSpawn(_amount);
        }
    }
}