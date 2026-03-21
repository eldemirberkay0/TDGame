using UnityEngine;

public class ArcherTower : ProjectileTower
{
    [SerializeField] private Animator archerAnimator;

    protected override void Shoot()
    {
        archerAnimator.SetTrigger("ShouldShoot");
        base.Shoot();
    }
}
