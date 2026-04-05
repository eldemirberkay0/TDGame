using UnityEngine;
using FlexTimer;

public class AnimatedProjectileTower : ProjectileTower
{
    [SerializeField] private Animator manAnimator;
    [SerializeField] private SpriteRenderer manRenderer;
    [SerializeField] private float animTime = 0.5f;
    private Timer animTimer;

    protected override void Start()
    {
        base.Start();
        manAnimator.speed = 1 / animTime / towerData.shootInterval;
        animTimer = new Timer(animTime / manAnimator.speed, () => base.Shoot());
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
