using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public EnemyData EnemyData { get; private set; }
    public EnemyHealth HealthComponent { get; private set; }
    public EnemyController ControllerComponent { get; private set; }

    private void Awake()
    {
        HealthComponent = GetComponent<EnemyHealth>();
        ControllerComponent = GetComponent<EnemyController>();
    }
}
