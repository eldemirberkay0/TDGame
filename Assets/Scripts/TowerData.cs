using UnityEngine;

public abstract class TowerData : ScriptableObject
{
    [field: Header("Base Tower Datas")]
    [field: SerializeField] public string TowerName { get; private set; }
    [field: SerializeField] public float Price { get; private set; }
}
