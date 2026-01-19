using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
    protected static SlowEffect CurrentSlow = null;

    public override void Apply(Enemy enemy)
    {
        this.enemy = enemy;
        timer = new Timer(duration);
        timer.OnTimerFinished = OnEffectFinished;
        timer.Start();
        enemy.EffectHandler.AddEffect(this);
        if (CurrentSlow != GetMaxSlow())
        {
            if (CurrentSlow != null) enemy.Controller.speedPercent += CurrentSlow.slowPercent;
            CurrentSlow = GetMaxSlow();
            enemy.Controller.speedPercent -= CurrentSlow.slowPercent;
        }
    }

    protected override void OnEffectFinished()
    {
        timer.OnTimerFinished = null;
        timer = null;
        enemy.EffectHandler.CurrentEffects.Remove(this);
        if (CurrentSlow == this) 
        {
            enemy.Controller.speedPercent += CurrentSlow.slowPercent;
            CurrentSlow = GetMaxSlow();
            if (CurrentSlow != null) { enemy.Controller.speedPercent -= CurrentSlow.slowPercent; }
        }
    }

    protected SlowEffect GetMaxSlow()
    {
        SlowEffect maxSlow = null;
        float maxSlowPerc = 0;
        List<SlowEffect> slows = new();
        foreach (Effect effect in enemy.EffectHandler.CurrentEffects)
        {
            if (effect is SlowEffect slowEffect)
            {
                slows.Add(slowEffect);
            }
        }
        foreach (SlowEffect slowEffect in slows)
        {
            if (slowEffect.slowPercent > maxSlowPerc) 
            { 
                maxSlow = slowEffect;
                maxSlowPerc = maxSlow.slowPercent; 
            }
        }
        return maxSlow;
    }
}