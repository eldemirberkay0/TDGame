using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static Action OnGameStarted = null;

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
}
