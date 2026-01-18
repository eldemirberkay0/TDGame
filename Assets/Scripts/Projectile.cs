using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected const float MIN_REACH_DISTANCE = 0.2f;
    protected Transform target;
    protected float speed;
    protected List<Effect> effectsToApply;

    protected void Update()
    {
        GoToTarget();
    }

    public virtual void InitProjectile(Transform target, float speed, List<Effect> effectsToApply)
    {
        this.target = target;
        this.speed = speed;
        this.effectsToApply = effectsToApply;
    }

    protected virtual void GoToTarget()
    {
        if (target == null)
        {
            DestroyProjectile();
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, target.position) <= MIN_REACH_DISTANCE)
        {
            OnHitTarget();
            DestroyProjectile();
        }
    }

    protected virtual void OnHitTarget()
    {
        foreach (Effect effect in effectsToApply) 
        {
            Effect effectToApply = effect.Clone();
            effectToApply.Apply(target.gameObject.GetComponent<Enemy>()); 
        }
    }

    protected void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
