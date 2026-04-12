using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int CurrentPointIndex { get; private set; } = 0;

    private Animator animator;
    private Enemy enemy;

    private float currentSpeed;
    private float speedMultiplier = 1;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        enemy.Renderer.color = new Color(1f, 1f, 1f, 1f);
        speedMultiplier = 1;
        CurrentPointIndex = 0;
        animator.speed = enemy.stats.speed * speedMultiplier;
        currentSpeed = enemy.stats.speed * speedMultiplier;
        if (currentSpeed < 0) { currentSpeed = 0; }
        if (animator.speed < 0) { animator.speed = 0; }
        SetFlip();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, LevelManager.Instance.LevelData.waypoints[CurrentPointIndex].position, Time.deltaTime * currentSpeed);
        if (Vector2.Distance(transform.position, LevelManager.Instance.LevelData.waypoints[CurrentPointIndex].position) < 0.1f)
        {
            CurrentPointIndex++;
            if (CurrentPointIndex >= LevelManager.Instance.LevelData.waypoints.Length) { Arrive(); }
            if (CurrentPointIndex < LevelManager.Instance.LevelData.waypoints.Length) { SetFlip(); }
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
        WaveManager.Instance.UpdateEnemyCounter();
        gameObject.SetActive(false);
    }

    private void SetFlip()
    {
        bool shouldTurnLeft = transform.position.x > LevelManager.Instance.LevelData.waypoints[CurrentPointIndex].transform.position.x && !enemy.Renderer.flipX;
        bool shouldTurnRight = transform.position.x < LevelManager.Instance.LevelData.waypoints[CurrentPointIndex].transform.position.x && enemy.Renderer.flipX;
        if (shouldTurnLeft || shouldTurnRight) { enemy.Renderer.flipX = !enemy.Renderer.flipX; }
    }
}
