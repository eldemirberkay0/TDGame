using System;
using System.Collections;
using System.Collections.Generic;
using FlexTimer;
using UnityEngine;

public class ProjectileTower : Tower
{
    [SerializeField] protected GameObject rangeIndicator;

    [Header("Optionals")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer manRenderer;
    [SerializeField] private float animTime = 0.5f;

    protected ProjectileTowerData towerData;
    protected List<Enemy> enemiesInRange = new List<Enemy>();
    protected Enemy targetEnemy = null;
    protected Timer reloadTimer = null;
    protected bool canShoot = false;

    protected void Awake()
    {
        towerData = TowerDatas[CurrentLevel - 1] as ProjectileTowerData;
        GetComponent<CircleCollider2D>().radius = towerData.range;
        reloadTimer = new Timer(towerData.shootInterval, () => canShoot = true);
        rangeIndicator.transform.localScale = new Vector3(towerData.range * 2, towerData.range * 2, 1f);
    }

    protected virtual void Start()
    {
        ObjectPooler.CreatePool(towerData.projectileData.prefab, 10);
        if (towerData.projectileData.hitVisual != null) { ObjectPooler.CreatePool(towerData.projectileData.hitVisual, 10); }
        if (animator != null) { animator.speed = 1 / animTime / towerData.shootInterval; }
        reloadTimer.Start();
    }

    protected void Update()
    {
        if (canShoot && targetEnemy != null)
        {
            targetEnemy = FindLeadEnemy();
            StartCoroutine(Shoot());
            canShoot = false;
            reloadTimer.Restart(towerData.shootInterval);
        }
        if (targetEnemy == null && enemiesInRange.Count > 0) { targetEnemy = FindLeadEnemy(); }
    }

    protected virtual IEnumerator Shoot()
    {
        if (manRenderer != null) { manRenderer.flipX = targetEnemy.transform.position.x < transform.position.x; }
        if (animator != null) { animator.SetTrigger("ShouldShoot"); }
        yield return new WaitForSeconds(animTime);
        GameObject projectile = ObjectPooler.GetObject(towerData.projectileData.prefab);
        projectile.transform.position = transform.position + towerData.projectileData.posOffset;
        if (targetEnemy != null) { projectile.GetComponent<Projectile>().InitProjectile(targetEnemy.transform, towerData.effects); ; }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Enemy.LAYER)
        {
            // Debug.Log(collision.gameObject);
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemiesInRange.Add(enemy);
            if (targetEnemy == null) { targetEnemy = enemiesInRange[0]; }
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Enemy.LAYER)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (targetEnemy == enemy) { targetEnemy = null; }
            enemiesInRange.Remove(enemy);
        }
    }

    public override void SetTowerInfo(bool isActive)
    {
        rangeIndicator.SetActive(isActive);
        UIManager.Instance.SetTowerControlPanel(isActive);
    }

    public override void Upgrade()
    {
        CurrentLevel++;
        towerData = TowerDatas[CurrentLevel - 1] as ProjectileTowerData;
        GetComponent<CircleCollider2D>().radius = towerData.range;
        rangeIndicator.transform.localScale = new Vector3(towerData.range * 2, towerData.range * 2, 1f);
        reloadTimer.Restart(towerData.shootInterval);
        if (animator != null) { animator.speed = 1 / animTime / towerData.shootInterval; }
    }

    protected void OnValidate()
    {
        if (towerData == null) return;
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if (collider != null) { collider.radius = towerData.range; }
    }

    protected Enemy FindLeadEnemy()
    {
        if (enemiesInRange.Count == 0) { return null; }
        Enemy leadEnemy = enemiesInRange[0];
        foreach (Enemy enemy in enemiesInRange)
        {
            if (enemy.Controller.CurrentPointIndex > leadEnemy.Controller.CurrentPointIndex) { leadEnemy = enemy; }
        }
        return leadEnemy;
    }

    protected void OnDestroy()
    {
        if (reloadTimer != null) { reloadTimer.Cancel(); }
        reloadTimer = null;
    }
}
