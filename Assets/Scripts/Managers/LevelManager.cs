using System;
using FlexTimer;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public static Action OnLevelStarted = null;
    public static Action OnLevelVictory = null;
    public Level LevelData { get; private set; }

    [SerializeField] private Level[] levels;

    private void Awake()
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
        GameManager.OnGameStarted += SetLevel;
    }

    private void SetLevel()
    {
        LevelData = levels[PlayerStats.CurrentLevel - 1];
        PlayerStats.SetGold(this.LevelData.initialGold);
        PlayerStats.SetLive(this.LevelData.health);
        UIManager.Instance.UpdateWave();
        Debug.Log("Level Started");
        OnLevelStarted?.Invoke();
    }

    public void DestroyTower()
    {
        PlayerStats.SetGold(PlayerStats.Gold + (float)(Tower.CurrentTower.TowerDatas[Tower.CurrentTower.CurrentLevel - 1].price * 0.3));
        Tower.CurrentTower.attachedNode.isEmpty = true;
        Tower.CurrentTower.SetTowerInfo(false);
        Tower.CurrentTower.attachedNode.gameObject.SetActive(true);
        if (Tower.CurrentTower is PassiveTower passiveTower) { passiveTower.CancelPassives(); }
        Destroy(Tower.CurrentTower.gameObject);
        Tower.CurrentTower = null;
    }

    public void UpgradeTower()
    {
        if (PlayerStats.Gold < Tower.CurrentTower.TowerDatas[Tower.CurrentTower.CurrentLevel].price) { return; }
        Tower.CurrentTower.Upgrade();
        PlayerStats.SetGold(PlayerStats.Gold - Tower.CurrentTower.TowerDatas[Tower.CurrentTower.CurrentLevel - 1].price);
        Tower.CurrentTower.SetTowerInfo(true);
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted -= SetLevel;
    }
}
