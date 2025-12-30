using UnityEngine;

[System.Serializable]
public abstract class Effect
{
    public abstract void Apply(Enemy enemy);
}

[System.Serializable]
public class DamageEffect : Effect
{
    [Header("Damage Effect")]
    [SerializeField] protected float damage;
    public override void Apply(Enemy enemy)
    {
        enemy.Health.TakeDamage(damage);
    }
}

[System.Serializable]
public class SlowEffect : Effect
{
    [Header("Slow Effect")]
    [SerializeField] protected float slowPercent;
    public override void Apply(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }
}