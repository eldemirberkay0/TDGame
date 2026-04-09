using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Enemy enemy;

    private float currentSpeed;
    private float speedMultiplier = 1;
    private int currentPointIndex = 0;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        animator.speed = enemy.stats.speed * speedMultiplier;
        currentSpeed = enemy.stats.speed * speedMultiplier;
        if (currentSpeed < 0) { currentSpeed = 0; }
        if (animator.speed < 0) { animator.speed = 0; }
        SetFlip();
    }

    private void Update()
    {
        Vector2 dir = (LevelManager.Instance.LevelData.waypoints[currentPointIndex].position - transform.position).normalized;
        Vector3 velocity = new Vector3(dir.x, dir.y, 0) * Time.deltaTime * currentSpeed;
        transform.position += velocity;

        if (Vector2.Distance(transform.position, LevelManager.Instance.LevelData.waypoints[currentPointIndex].position) < 0.1f)
        {
            // Debug.Log("Next waypoint");
            currentPointIndex++;
            if (currentPointIndex >= LevelManager.Instance.LevelData.waypoints.Length) { Arrive(); }
            if (currentPointIndex < LevelManager.Instance.LevelData.waypoints.Length) { SetFlip(); }
        }
    }

    public void ChangeSpeedPercent(float percent)
    {
        speedMultiplier += percent / 100;
        if (speedMultiplier < 0) { speedMultiplier = 0; }

        animator.speed = enemy.stats.speed * speedMultiplier;
        currentSpeed = enemy.stats.speed * speedMultiplier;
        if (currentSpeed < 0) { currentSpeed = 0; }
        if (animator.speed < 0) { animator.speed = 0; }
    }

    private void Arrive()
    {
        PlayerStats.SetLive(PlayerStats.Lives - 1);
        Destroy(gameObject);
    }

    private void SetFlip()
    {
        bool shouldTurnLeft = transform.position.x > LevelManager.Instance.LevelData.waypoints[currentPointIndex].transform.position.x && !spriteRenderer.flipX;
        bool shouldTurnRight = transform.position.x < LevelManager.Instance.LevelData.waypoints[currentPointIndex].transform.position.x && spriteRenderer.flipX;
        if (shouldTurnLeft || shouldTurnRight) { spriteRenderer.flipX = !spriteRenderer.flipX; }
    }
}
