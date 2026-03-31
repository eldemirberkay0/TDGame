using UnityEngine;
using System.Collections.Generic;

public class ProjectileHit : MonoBehaviour
{
    protected List<Effect> effectsToApply;
    protected Transform target;

    public virtual void Init(Transform target, List<Effect> effects)
    {
        this.effectsToApply = effects;
        this.target = target;
    }

    public virtual void OnArrivedTarget()
    {
        foreach (Effect effect in effectsToApply)
        {
            effect.Apply(target.gameObject.GetComponent<Enemy>());
        }
    }
}
