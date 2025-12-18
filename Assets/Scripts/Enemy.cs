using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public EnemyData Stats { get; private set; }
    public EnemyHealth Health { get; private set; }
    public EnemyController Controller { get; private set; }

    private void Awake()
    {
        Health = GetComponent<EnemyHealth>();
        Controller = GetComponent<EnemyController>();
    }
}
