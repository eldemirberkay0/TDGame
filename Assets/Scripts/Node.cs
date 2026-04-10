using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour, IPointerClickHandler
{
    public static Node CurrentNode = null;
    [HideInInspector] public bool isEmpty;

    public void BuildTower(TowerData tower)
    {
        if (PlayerStats.Gold < tower.price) { return; }
        Instantiate(tower.towerPrefab, CurrentNode.transform.position, CurrentNode.transform.rotation).GetComponent<Tower>().attachedNode = CurrentNode;
        PlayerStats.SetGold(PlayerStats.Gold - tower.price);
        UIManager.Instance.SetTowerMenu(false);
        CurrentNode.isEmpty = false;
        CurrentNode.gameObject.SetActive(false);
        CurrentNode = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("clicked");
            if (Tower.CurrentTower != null)
            {
                Tower.CurrentTower.SetTowerInfo(false);
                Tower.CurrentTower = null;
            }
            if (isEmpty)
            {
                CurrentNode = this;
                UIManager.Instance.SetTowerMenu(true);
            }
        }
    }
}
