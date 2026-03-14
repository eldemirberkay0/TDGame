using System.Collections.Generic;
using UnityEngine;
using FlexTimer;

[System.Serializable]
public abstract class Effect
{
    public abstract void Apply(Enemy enemy);
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
    protected Timer timer;
    protected Enemy targetEnemy;

    public override void Apply(Enemy enemy)
    {
        targetEnemy = enemy;
        timer = new Timer(duration, OnEffectFinished);
        targetEnemy.Controller.speedPercent -= slowPercent;
        timer.Start();
    }

    protected override void OnEffectFinished()
    {
        targetEnemy.Controller.speedPercent += slowPercent;
        timer.Cancel();
    }
}