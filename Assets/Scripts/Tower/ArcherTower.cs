using UnityEngine;
using FlexTimer;

public class ArcherTower : ProjectileTower
{
    [SerializeField] private Animator archerAnimator;
    private Timer animTimer;

    protected void Awake()
    {
        archerAnimator.speed = 1 / towerData.shootInterval;
        animTimer = new Timer(0.5f / archerAnimator.speed, () => base.Shoot());
    }

    protected override void Shoot()
    {
        animTimer.Start();
        archerAnimator.SetTrigger("ShouldShoot");
    }

    protected void OnDestroy()
    {
        animTimer.Cancel();
    }
}
