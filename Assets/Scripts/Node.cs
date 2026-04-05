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
            UIManager.Instance.SetTowerMenu(true);
        }
    }

    public void BuildTower(TowerData tower)
    {
        Instantiate(tower.towerPrefab, CurrentNode.transform.position, CurrentNode.transform.rotation);
        UIManager.Instance.SetTowerMenu(false);
        PlayerStats.SetGold(PlayerStats.Gold - tower.price);
    }
}
