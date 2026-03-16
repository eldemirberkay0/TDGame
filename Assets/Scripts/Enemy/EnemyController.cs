using System;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;

    private Animator animator;

    private Enemy enemy;
    private float currentSpeed;
    private float speedMultiplier = 1;
    private int currentPointIndex = 0;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.speed = enemy.stats.speed * speedMultiplier;
        currentSpeed = enemy.stats.speed * speedMultiplier;
        if (currentSpeed < 0) { currentSpeed = 0; }
        if (animator.speed < 0) { animator.speed = 0; }
    }

    private void Update()
    {
        Vector2 dir = (waypoints[currentPointIndex].position - transform.position).normalized;
        Vector3 velocity = new Vector3(dir.x, dir.y, 0) * Time.deltaTime * currentSpeed;
        transform.position += velocity;

        if (Vector2.Distance(transform.position, waypoints[currentPointIndex].position) < 0.1f)
        {
            Debug.Log("Next waypoint");
            currentPointIndex++;
            if (currentPointIndex >= waypoints.Length) { Arrive(); }
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
        Destroy(gameObject);
    }
}
