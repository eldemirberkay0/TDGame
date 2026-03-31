using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileData projectileData;
    public ProjectileMovement MovementController { get; private set; }
    public ProjectileHit HitController { get; private set; }

    protected void Awake()
    {
        MovementController = GetComponent<ProjectileMovement>();
        HitController = GetComponent<ProjectileHit>();
    }

    public virtual void InitProjectile(Transform target, float speed, bool isGuided, List<Effect> effectsToApply)
    {
        MovementController.Init(target, speed, isGuided);
        HitController.Init(target, effectsToApply);
        gameObject.SetActive(true);
    }

    protected void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }
}
