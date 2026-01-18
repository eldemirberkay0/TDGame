using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private Enemy enemy;
    private int currentPointIndex = 0;
    private float currentSpeed;
    private float speedPercent = 100;
    public float SpeedPercent
    {
        get { return speedPercent; }
        set
        {
            if (value < 0) { speedPercent = 0; }
            else { speedPercent = value; }
        }
    }

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
        Vector3 velocity = new Vector3(dir.x, dir.y, 0) * Time.deltaTime * currentSpeed * speedPercent / 100;
        transform.position += velocity;
        if (Vector2.Distance(transform.position, waypoints[currentPointIndex].position) < 0.1f)
        {
            Debug.Log("Arrived");
            currentPointIndex++;
        }
        if (currentPointIndex >= waypoints.Length) { Destroy(gameObject); }
    }
}
