using UnityEngine;

public interface IEffect
{
    void Apply(IDamageable enemy);
}

[System.Serializable]
public class DamageEffect : IEffect
{
    [SerializeField] private float _damage;
    public void Apply(IDamageable damageable)
    {
        damageable.TakeDamage(_damage);
    }
}
