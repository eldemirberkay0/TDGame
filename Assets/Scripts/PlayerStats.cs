using System;

public static class PlayerStats
{
    public static Action OnGameOver = null;

    public static float Gold { get; private set; } = 999;
    public static int Lives { get; private set; } = 999;
    public static int CurrentLevel { get; private set; } = 999;

    public static void SetGold(float amount)
    {
        Gold = amount;
        UIManager.Instance.UpdateCoin();
    }

    public static void SetLive(int amount)
    {
        Lives = amount;
        UIManager.Instance.UpdateLive();
        if (Lives <= 0) { OnGameOver?.Invoke(); }
    }

    public static void SetCurrentLevel(int level) => CurrentLevel = level;
}
