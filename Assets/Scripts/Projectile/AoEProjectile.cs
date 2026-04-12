using System.Collections.Generic;
using UnityEngine;

public class AoEProjectile : ProjectileHit
{
    protected List<Enemy> enemies = new();

    protected override void Awake()
    {
        base.Awake();
        GetComponent<CircleCollider2D>().radius = projectile.projectileData.effectRadius;
    }

    public override void OnArrivedTarget()
    {
        HandleHitVisual();
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            foreach (Effect effect in effectsToApply)
            {
                effect.Apply(enemies[i]);
            }
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Enemy.LAYER)
        {
            enemies.Add(collision.gameObject.GetComponent<Enemy>());
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Enemy.LAYER)
        {
            enemies.Remove(collision.gameObject.GetComponent<Enemy>());
        }
    }
}
