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

    public void BuildTower(GameObject tower)
    {
        GameObject temp = Instantiate(tower, CurrentNode.transform.position, CurrentNode.transform.rotation);
        // temp.SetActive(true);
        UIManager.Instance.CloseTowerMenu();
    }
}
