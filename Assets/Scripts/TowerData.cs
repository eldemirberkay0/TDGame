using UnityEngine;

public abstract class TowerData : ScriptableObject
{
    [Header("Base Tower Datas")]
    public string towerName;
    public float range;
    public float price;
}
