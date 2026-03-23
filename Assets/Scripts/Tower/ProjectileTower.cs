using System.Collections.Generic;
using UnityEngine;

public class ProjectileTower : Tower<ProjectileTowerData>
{
    protected float reloadTime;
    protected List<Enemy> enemiesInRange = new List<Enemy>();
    protected Enemy targetEnemy = null;

    protected void Update()
    {
        if (reloadTime <= 0 && targetEnemy != null)
        {
            Shoot();
            reloadTime = towerData.shootInterval;
        }
        reloadTime -= Time.deltaTime;
        if (targetEnemy == null && enemiesInRange.Count > 0) { targetEnemy = enemiesInRange[0]; }
    }

    protected virtual void Shoot()
    {
        GameObject projectile = Instantiate(towerData.projectilePrefab, transform.position + towerData.projectilePosOffset, transform.rotation);
        projectile.SetActive(false);
        projectile.GetComponent<Projectile>().InitProjectile(targetEnemy.transform, towerData.projectileSpeed, towerData.effects);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7) // Enemy layer is 7
        {
            Debug.Log(collision.gameObject);
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemiesInRange.Add(enemy);
            if (targetEnemy == null) { targetEnemy = enemy; }
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7) // Enemy layer is 7
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (targetEnemy == enemy) { targetEnemy = null; }
            enemiesInRange.Remove(enemy);
        }
    }

    protected virtual void OnValidate()
    {
        if (towerData == null) return;
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if (collider != null) { collider.radius = towerData.range; }
    }
}
