using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Bad and unflexible UI manager but enough for now
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text goldsText;
    [SerializeField] private GameObject levelInfoCanvas;
    [SerializeField] private GameObject towerSelectionMenu;
    [SerializeField] private GameObject startButton;

    public static UIManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        GameManager.OnGameStarted += ShowLevelUI;
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

    private void ShowLevelUI()
    {
        startButton.SetActive(false);
        levelInfoCanvas.SetActive(true);
        UpdateCoin();
        UpdateLive();
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted -= ShowLevelUI;
    }

    public void UpdateCoin() { goldsText.text = ((int)PlayerStats.Gold).ToString(); }
    public void UpdateLive() { livesText.text = PlayerStats.Lives.ToString(); }
}