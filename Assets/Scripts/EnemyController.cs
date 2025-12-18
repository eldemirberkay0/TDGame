using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    private Enemy _enemy;
    private int _currentPointIndex = 0;
    private float _currentSpeed;

    void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    void Start()
    {
        _currentSpeed = _enemy.Stats.Speed;
    }

    void Update()
    {
        Vector2 dir = (_waypoints[_currentPointIndex].position - transform.position).normalized;
        Vector3 velocity = new Vector3(dir.x, dir.y, 0) * Time.deltaTime * _currentSpeed;
        transform.position += velocity;
        if (Vector2.Distance(transform.position, _waypoints[_currentPointIndex].position) < 0.1f)
        {
            Debug.Log("Arrived");
            _currentPointIndex++;
        }
        if (_currentPointIndex >= _waypoints.Length) { Destroy(gameObject); }
    }
}
