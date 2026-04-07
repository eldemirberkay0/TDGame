using System.Collections.Generic;
using UnityEngine;
using FlexTimer;

[System.Serializable]
public abstract class Effect
{
    public abstract void Apply(Enemy enemy);
    public abstract Effect Clone();
}

[System.Serializable]
public abstract class TimerEffect : Effect
{
    protected abstract void OnEffectFinished();
    protected Timer timer;
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

    public override Effect Clone() => (Effect)this.MemberwiseClone();
}

[System.Serializable]
public class SlowEffect : TimerEffect
{
    [Header("Slow Effect")]
    [SerializeField] protected float slowPercent;
    [SerializeField] protected float duration;

    protected Enemy targetEnemy;

    public override void Apply(Enemy enemy)
    {
        if (timer != null)
        {
            timer.Cancel();
            timer = null;
        }
        targetEnemy = null;

        if (enemy.EffectHandler.CurrentEffects.Count > 0)
        {
            for (int i = enemy.EffectHandler.CurrentEffects.Count - 1; i >= 0; i--)
            {
                Effect currentEffect = enemy.EffectHandler.CurrentEffects[i];
                if (currentEffect is SlowEffect currentSlow)
                {
                    if (currentSlow.slowPercent > this.slowPercent) { return; }
                    currentSlow.OnEffectFinished();
                    enemy.EffectHandler.RemoveEffect(currentSlow);
                }
            }
        }

        targetEnemy = enemy;
        timer = new Timer(duration, OnEffectFinished);
        targetEnemy.Controller.ChangeSpeedPercent(-slowPercent);
        targetEnemy.EffectHandler.AddEffect(this);
        timer.Start();
    }

    protected override void OnEffectFinished()
    {
        timer.Cancel();
        timer = null;
        if (targetEnemy == null) { return; }
        if (targetEnemy.EffectHandler.CurrentEffects.Contains(this) && targetEnemy.isActiveAndEnabled) { targetEnemy.Controller.ChangeSpeedPercent(slowPercent); }
    }

    public override Effect Clone() => (Effect)this.MemberwiseClone();
}