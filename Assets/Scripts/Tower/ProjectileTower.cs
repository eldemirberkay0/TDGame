using System;
using System.Collections;
using System.Collections.Generic;
using FlexTimer;
using UnityEngine;

public class ProjectileTower : Tower
{
    protected new ProjectileTowerData towerData => base.towerData as ProjectileTowerData;

    [SerializeField] protected Transform projectileContainer;
    [SerializeField] protected GameObject rangeIndicator;

    [Header("Optionals")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer manRenderer;
    [SerializeField] private float animTime = 0.5f;

    protected List<Enemy> enemiesInRange = new List<Enemy>();
    protected Enemy targetEnemy = null;
    protected Timer reloadTimer = null;
    protected bool canShoot = false;

    protected void Awake()
    {
        GetComponent<CircleCollider2D>().radius = towerData.range;
        reloadTimer = new Timer(towerData.shootInterval, () => canShoot = true);
        rangeIndicator.transform.localScale = new Vector3(towerData.range * 2, towerData.range * 2, 1f);
    }

    protected virtual void Start()
    {
        ObjectPooler.CreatePool(towerData.projectileData.prefab, 10, projectileContainer);
        if (animator != null) { animator.speed = 1 / animTime / towerData.shootInterval; }
        reloadTimer.Start();
    }

    protected void Update()
    {
        if (canShoot && targetEnemy != null)
        {
            StartCoroutine(Shoot());
            canShoot = false;
            reloadTimer.Restart(towerData.shootInterval);
        }
        if (targetEnemy == null && enemiesInRange.Count > 0) { targetEnemy = enemiesInRange[0]; }
    }

    protected virtual IEnumerator Shoot()
    {
        if (manRenderer != null) { manRenderer.flipX = targetEnemy.transform.position.x < transform.position.x; }
        GameObject projectile = ObjectPooler.GetObject(projectileContainer);
        projectile.transform.position = transform.position + towerData.projectileData.posOffset;
        if (animator != null) { animator.SetTrigger("ShouldShoot"); }
        yield return new WaitForSeconds(animTime);
        if (targetEnemy != null) { projectile.GetComponent<Projectile>().InitProjectile(targetEnemy.transform, towerData.effects); ; }
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

    public override void SetTowerInfo(bool isActive)
    {
        rangeIndicator.SetActive(isActive);
    }

    protected void OnValidate()
    {
        if (towerData == null) return;
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if (collider != null) { collider.radius = towerData.range; }
    }

    protected void OnDestroy()
    {
        if (reloadTimer != null) { reloadTimer.Cancel(); }
        reloadTimer = null;
    }
}
