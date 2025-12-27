using UnityEngine;

public abstract class Tower<T> : MonoBehaviour where T : TowerData
{
    [SerializeField] protected T towerData;
}
