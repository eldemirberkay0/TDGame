using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Tower : MonoBehaviour, IPointerDownHandler
{
    public static Tower CurrentTower;

    [SerializeField] protected TowerData towerData;

    public abstract void SetTowerInfo(bool isActive);

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CurrentTower != null) { CurrentTower.SetTowerInfo(false); }
        CurrentTower = this;
        SetTowerInfo(true);
    }
}
