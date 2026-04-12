using UnityEngine;

public abstract class TowerData : ScriptableObject
{
    [Header("Base Tower Datas")]
    public GameObject towerPrefab;
    public int price;
    public Vector3 buildOffset;
}
