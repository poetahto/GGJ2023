using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float value;
    [SerializeField] private float maxHealth;

    public UnityEvent<float> onDamage;
    public UnityEvent<float> onHeal;

    public void Damage(float amount)
    {
        value -= amount;
        onDamage.Invoke(amount);
    }

    public void Heal(float amount)
    {
        value += amount;
        onHeal.Invoke(amount);
    }

    public float CurrentHealth => value;
    
    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }
}