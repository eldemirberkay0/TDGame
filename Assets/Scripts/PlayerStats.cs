using System;

public static class PlayerStats
{
    public static Action OnGameOver;

    public static float Coin { get; private set; }
    public static int Lives { get; private set; }
    public static int MaxLevel { get; private set; }
    public static int CurrentLevel { get; private set; }

    public static void DecreaseHealth(int amount)
    {
        Lives -= amount;
        if (amount <= 0) { GameOver(); }
    }

    private static void GameOver()
    {
        OnGameOver?.Invoke();
    }

    public static void SetCoin(float amount)
    {
        Coin = amount;
        UIManager.Instance.UpdateCoin();
    }

    public static void SetLive(int amount)
    {
        Lives = amount;
        UIManager.Instance.UpdateLive();
    }

    public static void SetCurrentLevel(int level) => CurrentLevel = level;
}
