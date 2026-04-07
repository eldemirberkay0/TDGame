using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    public int initialGold;
    public int health;
    public Wave[] waves;
}

[System.Serializable]
public struct Wave
{
    public float durationToStart;
    public float maxTimeReward;
    public EnemyGroup[] enemyGroups;
    public float durationToNext;
}

[System.Serializable]
public struct EnemyGroup
{
    public float delay;
    public GameObject enemy;
    public int count;
    public float interval;
}