using JetBrains.Annotations;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private Enemy enemy;
    private int currentPointIndex = 0;
    private float currentSpeed;
    public float speedPercent = 100;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void Start()
    {
        currentSpeed = enemy.stats.baseSpeed;
    }

    void Update()
    {
        Vector2 dir = (waypoints[currentPointIndex].position - transform.position).normalized;
        float actualSpeed = speedPercent;
        if (actualSpeed < 0) { actualSpeed = 0; }
        Vector3 velocity = new Vector3(dir.x, dir.y, 0) * Time.deltaTime * currentSpeed * actualSpeed / 100;
        transform.position += velocity;
        if (Vector2.Distance(transform.position, waypoints[currentPointIndex].position) < 0.1f)
        {
            Debug.Log("Arrived");
            currentPointIndex++;
        }
        if (currentPointIndex >= waypoints.Length) { Destroy(gameObject); }
    }
}
