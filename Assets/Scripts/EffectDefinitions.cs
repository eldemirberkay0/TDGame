using System.Collections;
using UnityEngine;

[System.Serializable]
public abstract class Effect
{
    public abstract void Apply(Enemy enemy);
    public virtual Effect Clone()
    {
        return (Effect)this.MemberwiseClone();
    }
}

[System.Serializable]
public abstract class TimerEffect : Effect
{
    protected abstract void OnEffectFinished();
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
public class SlowEffect : TimerEffect
{
    [Header("Slow Effect")]
    [SerializeField] protected float slowPercent;
    [SerializeField] protected float duration;
    Enemy enemy;
    protected Timer timer;
    
    public override void Apply(Enemy enemy)
    {
        timer = new Timer(duration);
        timer.OnTimerFinished = OnEffectFinished;
        enemy.Controller.SpeedPercent -= slowPercent;
        timer.Start();
        this.enemy = enemy;
        enemy.EffectHandler.AddEffect(this);
    }

    protected override void OnEffectFinished()
    {
        timer.OnTimerFinished = null;
        timer = null;
        enemy.Controller.SpeedPercent += slowPercent;
    }
}