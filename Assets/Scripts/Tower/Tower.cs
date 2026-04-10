using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Tower : MonoBehaviour, IPointerDownHandler
{
    public static Tower CurrentTower;
    public int CurrentLevel { get; protected set; } = 1;

    [field: SerializeField] public TowerData[] TowerDatas { get; protected set; }

    [HideInInspector] public Node attachedNode = null;

    public abstract void SetTowerInfo(bool isActive);

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Node.CurrentNode != null)
        {
            UIManager.Instance.SetTowerMenu(false);
            Node.CurrentNode = null;
        }

        if (CurrentTower != null) { CurrentTower.SetTowerInfo(false); }
        CurrentTower = this;
        CurrentTower.SetTowerInfo(true);
    }

    public abstract void Upgrade();
}
