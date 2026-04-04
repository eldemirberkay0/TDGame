using UnityEngine;

public class Node : MonoBehaviour
{
    private bool isEmpty = true;
    public static Node CurrentNode;

    public void OnClick()
    {
        Debug.Log("clicked");
        if (isEmpty)
        {
            CurrentNode = this;
            UIManager.Instance.ShowTowerMenu();
        }
    }

    public void BuildTower(TowerData tower)
    {
        Instantiate(tower.towerPrefab, CurrentNode.transform.position, CurrentNode.transform.rotation);
        UIManager.Instance.CloseTowerMenu();
        PlayerStats.SetGold(PlayerStats.Gold - tower.price);
    }
}
