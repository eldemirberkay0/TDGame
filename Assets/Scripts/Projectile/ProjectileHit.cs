using UnityEngine;
using System.Collections.Generic;
using FlexTimer;

public class ProjectileHit : MonoBehaviour
{
    protected List<Effect> effectsToApply;
    protected Transform target;
    protected Projectile projectile;

    protected virtual void Awake()
    {
        projectile = GetComponent<Projectile>();
    }

    public virtual void Init(Transform target, List<Effect> effects)
    {
        this.effectsToApply = effects;
        this.target = target;
    }

    public virtual void OnArrivedTarget()
    {
        HandleHitVisual();
        foreach (Effect effect in effectsToApply)
        {
            Effect effectClone = effect.Clone();
            effectClone.Apply(target.gameObject.GetComponent<Enemy>());
        }
    }

    protected void HandleHitVisual()
    {
        if (projectile.projectileData.hitVisual != null)
        {
            GameObject hitVisual = ObjectPooler.GetObject(projectile.projectileData.hitVisual);
            hitVisual.transform.position = transform.position;
            hitVisual.SetActive(true);
            TimerManager.RegisterEvent(0.5f, () => hitVisual.SetActive(false));
        }
    }
}
