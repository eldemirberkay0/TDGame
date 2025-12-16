using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyHealth HealthComponent { get; private set; }
    public EnemyController ControllerComponent { get; private set; }

    private void Awake()
    {
        HealthComponent = GetComponent<EnemyHealth>();
        ControllerComponent = GetComponent<EnemyController>();
    }
}
