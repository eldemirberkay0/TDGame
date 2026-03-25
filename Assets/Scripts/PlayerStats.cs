using System;

public static class PlayerStats
{
    public static Action OnGameOver;

    public static int Gold = 10;
    public static int Health = 10;
    public static int MaxLevel = 0;
    public static int CurrentLevel = 0;

    public static void DecreaseHealth(int amount)
    {
        Health -= amount;
        if (amount <= 0) { GameOver(); }
    }

    private static void GameOver()
    {
        OnGameOver?.Invoke();
    }

    public static void SetGold(int amount) => Gold = amount;
    public static void SetHealth(int amount) => Health = amount;
}
