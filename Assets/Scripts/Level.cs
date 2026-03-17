using UnityEngine;

[System.Serializable]
public struct Level
{
    public Wave[] waves;
}

[System.Serializable]
public struct Wave
{
    public float delay;
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