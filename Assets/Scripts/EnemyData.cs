using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Scriptable Objects/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float maxHealth;
    public float baseSpeed;
}
