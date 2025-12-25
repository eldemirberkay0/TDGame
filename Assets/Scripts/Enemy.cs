using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData stats;
    public EnemyHealth Health { get; private set; }
    public EnemyController Controller { get; private set; }

    private void Awake()
    {
        Controller = GetComponent<EnemyController>();
        Health = GetComponent<EnemyHealth>();
    }
}
