using UnityEngine;

public abstract class TowerData : ScriptableObject
{
    [Header("Base Tower Datas")]
    public GameObject towerPrefab;
    public float price;
}
