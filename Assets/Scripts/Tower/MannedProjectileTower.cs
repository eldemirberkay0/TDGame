using UnityEngine;
using FlexTimer;

public class MannedProjectileTower : ProjectileTower
{
    [SerializeField] private Animator manAnimator;
    [SerializeField] private SpriteRenderer manRenderer;
    private Timer animTimer;

    protected override void Start()
    {
        base.Start();
        manAnimator.speed = 1 / towerData.animTime / towerData.shootInterval;
        animTimer = new Timer(towerData.animTime / manAnimator.speed, () => base.Shoot());
    }

    protected override void Shoot()
    {
        animTimer.Start();
        manAnimator.SetTrigger("ShouldShoot");
        manRenderer.flipX = targetEnemy.transform.position.x < transform.position.x;
    }

    protected void OnDestroy()
    {
        animTimer.Cancel();
    }
}
