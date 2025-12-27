using UnityEngine;

public interface IEffect
{
    void Apply(Enemy enemy);
}

[System.Serializable]
public class DamageEffect : IEffect
{
    [Header("Damage Effect")]
    [SerializeField] private float damage;
    public void Apply(Enemy enemy)
    {
        enemy.Health.TakeDamage(damage);
    }
}