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

    public void InitProjectile(Transform target)
    {
        MovementController.Init(target, projectileData.speed, projectileData.isGuided);
        HitController.Init(target, projectileData.effects);
        gameObject.SetActive(true);
    }

    protected void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }
}
