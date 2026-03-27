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
        if (target == null)
        {
            DestroyProjectile();
            return;
        }

        GoToTarget();

        if (Vector2.Distance(transform.position, target.position) <= MIN_REACH_DISTANCE)
        {
            OnHitTarget();
            DestroyProjectile();
        }
    }

    public virtual void InitProjectile(Transform target, float speed, List<Effect> effectsToApply)
    {
        this.target = target;
        this.speed = speed;
        this.effectsToApply = effectsToApply;
        gameObject.SetActive(true);
    }

    protected virtual void GoToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    protected virtual void OnHitTarget()
    {
        foreach (Effect effect in effectsToApply)
        {
            effect.Apply(target.gameObject.GetComponent<Enemy>());
        }
    }

    protected void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }
}
