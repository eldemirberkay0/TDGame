using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    protected const float MIN_REACH_DISTANCE = 0.2f;
    protected Projectile projectile;
    protected Vector3 targetPos;

    protected Transform target;
    protected float speed;
    protected bool isGuided;

    protected void Awake()
    {
        projectile = GetComponent<Projectile>();
    }

    protected void Update()
    {
        if (isGuided && target == null)
        {
            DestroyProjectile();
            return;
        }
        if (isGuided) { targetPos = target.position; }
        GoToTarget();
        if (Vector2.Distance(transform.position, targetPos) <= MIN_REACH_DISTANCE)
        {
            projectile.HitController.OnArrivedTarget();
            DestroyProjectile();
        }
    }

    public virtual void Init(Transform target, float speed, bool isGuided)
    {
        this.target = target;
        this.speed = speed;
        this.isGuided = isGuided;
        targetPos = target.position;
    }

    protected virtual void GoToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        transform.right = targetPos - transform.position;
    }

    protected void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }
}
