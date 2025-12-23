using UnityEngine;

public interface IEffect
{
    void Apply(IDamageable damageable);
}

[System.Serializable]
public class DamageEffect : IEffect
{
    [SerializeField] private float damage;
    public void Apply(IDamageable damageable)
    {
        damageable.TakeDamage(damage);
    }
}
