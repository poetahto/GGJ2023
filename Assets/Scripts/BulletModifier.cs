using System;
using UnityEngine;

public abstract class BulletModifier
{
    public GameObject Bullet { get; set; }

    public abstract BulletModifier Copy();
    public virtual void Initialize() {}
    public virtual void Update() {}
    public virtual void OnHit(Collider col) {}
}