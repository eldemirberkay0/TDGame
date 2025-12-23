using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public EnemyData Stats { get; private set; }
}
