using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Scriptable Objects/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float Speed { get; private set; } 
}
