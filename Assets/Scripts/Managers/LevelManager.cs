using System;
using FlexTimer;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public static Action OnLevelStarted;
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

        Debug.Log("Level Started");
        OnLevelStarted?.Invoke();
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted -= SetLevel;
    }
}
