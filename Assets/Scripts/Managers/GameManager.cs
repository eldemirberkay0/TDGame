using System;
using FlexTimer;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static Action OnGameStarted = null;

    private void OnEnable()
    {
        LevelManager.OnLevelVictory += Victory;
        PlayerStats.OnGameOver += GameOver;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void StartGame()
    {
        PlayerStats.SetCurrentLevel(1);
        Debug.Log("Game Started");
        OnGameStarted?.Invoke();
    }

    public void GameOver()
    {
        TimerManager.RemoveAllTimers();
        ObjectPooler.ClearPool();
        SceneManager.LoadScene("GameOverScene");
    }

    public void Restart()
    {
        TimerManager.RemoveAllTimers();
        ObjectPooler.ClearPool();
        SceneManager.LoadScene("GameScene");
    }

    public void Victory()
    {
        TimerManager.RemoveAllTimers();
        ObjectPooler.ClearPool();
        SceneManager.LoadScene("LevelVictoryScene");
    }

    private void OnDisable()
    {
        LevelManager.OnLevelVictory -= Victory;
        PlayerStats.OnGameOver -= GameOver;
    }
}
