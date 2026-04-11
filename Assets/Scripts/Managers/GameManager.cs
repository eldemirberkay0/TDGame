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
        OnGameStarted?.Invoke();
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
        TimerManager.RemoveAllTimers();
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
        TimerManager.RemoveAllTimers();
    }

    public void Victory()
    {
        SceneManager.LoadScene("LevelVictoryScene");
        TimerManager.RemoveAllTimers();
    }

    private void OnDisable()
    {
        LevelManager.OnLevelVictory -= Victory;
        PlayerStats.OnGameOver -= GameOver;
    }
}
