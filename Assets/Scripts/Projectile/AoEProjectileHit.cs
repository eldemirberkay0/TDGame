using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AoEProjectileHit : ProjectileHit
{
    protected AoEProjectileData aoeProjectileData;
    protected List<Enemy> enemies = new();

    protected void Awake()
    {
        aoeProjectileData = GetComponent<Projectile>().projectileData as AoEProjectileData;
        GetComponent<CircleCollider2D>().radius = aoeProjectileData.effectRadius;
    }

    public override void OnArrivedTarget()
    {
        for (int i = enemies.Count - 1; i > 0; i--)
        {
            foreach (Effect effect in effectsToApply)
            {
                effect.Apply(enemies[i]);
            }
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7) // Enemy layer is 7
        {
            enemies.Add(collision.gameObject.GetComponent<Enemy>());
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7) // Enemy layer is 7
        {
            enemies.Remove(collision.gameObject.GetComponent<Enemy>());
        }
    }
}
