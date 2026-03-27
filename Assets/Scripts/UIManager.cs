using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject towerSelectionMenu;
    public static UIManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void UpdateLevelInfoUI()
    {
        Debug.Log("New player gold: " + PlayerStats.Gold + ", new player health: " + PlayerStats.Health);
    }

    public void ShowTowerMenu()
    {
        towerSelectionMenu.transform.position = Node.CurrentNode.transform.position;
        towerSelectionMenu.SetActive(true);
    }

    public void CloseTowerMenu()
    {
        towerSelectionMenu.SetActive(false);
    }
}
