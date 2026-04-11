using UnityEngine;
using FlexTimer;

[System.Serializable]
public abstract class Effect
{
    public abstract void Apply(Enemy enemy);
    public Effect Clone() => (Effect)this.MemberwiseClone();
}

[System.Serializable]
public abstract class TimerEffect : Effect
{
    protected abstract void OnEffectFinished();
    protected Timer timer = null;
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

    protected Enemy targetEnemy = null;

    public override void Apply(Enemy enemy)
    {
        if (enemy.EffectHandler.CurrentEffects.Count > 0)
        {
            for (int i = enemy.EffectHandler.CurrentEffects.Count - 1; i >= 0; i--)
            {
                Effect currentEffect = enemy.EffectHandler.CurrentEffects[i];
                if (currentEffect is SlowEffect currentSlow)
                {
                    if (currentSlow.slowPercent > this.slowPercent) { return; }
                    else if (currentSlow.slowPercent == this.slowPercent)
                    {
                        currentSlow.timer.Restart();
                        return;
                    }
                    currentSlow.OnEffectFinished();
                    enemy.EffectHandler.RemoveEffect(currentSlow);
                }
            }
        }

        targetEnemy = enemy;
        timer = new Timer(duration, OnEffectFinished);
        targetEnemy.Controller.ChangeSpeedPercent(-slowPercent);
        targetEnemy.Renderer.color = new Color(0, 0.9f, 1f, 1f);
        targetEnemy.EffectHandler.AddEffect(this);
        timer.Start();
    }

    protected override void OnEffectFinished()
    {
        timer.Cancel();
        timer = null;
        if (targetEnemy == null) { return; }
        if (targetEnemy.EffectHandler.CurrentEffects.Contains(this))
        {
            targetEnemy.EffectHandler.RemoveEffect(this);
            if (targetEnemy.isActiveAndEnabled)
            {
                targetEnemy.Controller.ChangeSpeedPercent(slowPercent);
                targetEnemy.Renderer.color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }
}