using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected const float MIN_REACH_DISTANCE = 0.2f;
    protected Transform target;
    protected float speed;
    protected bool isGuided;
    protected Vector3 targetPos;
    protected List<Effect> effectsToApply;

    protected virtual void Update()
    {
        if (isGuided && target == null)
        {
            DestroyProjectile();
            return;
        }
        if (isGuided) { targetPos = target.position; }

        GoToTarget();

        if (Vector2.Distance(transform.position, targetPos) <= MIN_REACH_DISTANCE)
        {
            OnHitTarget();
            DestroyProjectile();
        }
    }

    public virtual void InitProjectile(Transform target, float speed, List<Effect> effectsToApply, bool isGuided)
    {
        this.target = target;
        this.speed = speed;
        this.effectsToApply = effectsToApply;
        this.isGuided = isGuided;
        targetPos = target.position;
        gameObject.SetActive(true);
    }

    protected virtual void GoToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        transform.right = targetPos - transform.position;
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
