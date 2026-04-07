using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour, IPointerClickHandler
{
    private bool isEmpty = true;
    public static Node CurrentNode = null;

    public void BuildTower(TowerData tower)
    {
        Instantiate(tower.towerPrefab, CurrentNode.transform.position, CurrentNode.transform.rotation);
        UIManager.Instance.SetTowerMenu(false);
        PlayerStats.SetGold(PlayerStats.Gold - tower.price);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("clicked");
            if (isEmpty)
            {
                CurrentNode = this;
                UIManager.Instance.SetTowerMenu(true);
            }
        }
    }
}
