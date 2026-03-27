using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject towerMenuCanvas;
    [SerializeField] private LevelManager levelManager;
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
        towerMenuCanvas.SetActive(true);
        levelManager.pos = transform.position;
    }
}
