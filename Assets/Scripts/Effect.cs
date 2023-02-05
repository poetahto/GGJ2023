using UnityEngine;

public abstract class Effect
{
    public abstract string Name { get; }
    public abstract string Description { get; } 

    public GameObject Player { get; set; }
        
    public abstract void Initialize();
    public virtual void Update() {}
    public abstract void Shutdown();
}