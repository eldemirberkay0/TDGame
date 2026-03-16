using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Scriptable Objects/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float health;
    public float speed;
}
