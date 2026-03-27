using UnityEngine;
using FlexTimer;

public class ArcherTower : ProjectileTower
{
    [SerializeField] private Animator archerAnimator;
    [SerializeField] private SpriteRenderer archerRenderer;
    private Timer animTimer;

    protected override void Start()
    {
        base.Start();
        animTimer = new Timer(0.5f / archerAnimator.speed, () => base.Shoot());
        archerAnimator.speed = 1 / towerData.shootInterval;
    }

    protected override void Shoot()
    {
        animTimer.Start();
        archerAnimator.SetTrigger("ShouldShoot");
        archerRenderer.flipX = targetEnemy.transform.position.x < transform.position.x;
    }

    protected void OnDestroy()
    {
        animTimer.Cancel();
    }
}
