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
    public float time;
    public float maxTimeReward;
    public EnemyGroup[] enemyGroups;
}

[System.Serializable]
public struct EnemyGroup
{
    public float delay;
    public GameObject enemy;
    public int count;
    public float interval;
}