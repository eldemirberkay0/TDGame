using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private int currentPointIndex = 0;
    private int currentSpeed = 2;

    void Start()
    {

    }

    void Update()
    {
        Vector2 dir = (waypoints[currentPointIndex].position - transform.position).normalized;
        Vector3 velocity = new Vector3(dir.x, dir.y, 0) * Time.deltaTime * currentSpeed;
        transform.position += velocity;
        if (Vector2.Distance(transform.position, waypoints[currentPointIndex].position) < 0.1f)
        {
            Debug.Log("Arrived");
            currentPointIndex++;
        }
        if (currentPointIndex >= waypoints.Length) { Destroy(gameObject); }
    }
}
