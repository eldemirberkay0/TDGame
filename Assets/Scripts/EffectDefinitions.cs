using UnityEngine;

public interface IEffect
{
    public void Apply(Enemy enemy);
}

[System.Serializable]
public class DamageEffect : IEffect
{
    [SerializeField] private float damage;
    public void Apply(Enemy enemy)
    {
        enemy.HealthComponent.TakeDamage(damage);
    }
}
