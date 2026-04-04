using FlexTimer;
using UnityEngine;

[System.Serializable]
public abstract class Passive
{
    public abstract void Use();
}

[System.Serializable]
public abstract class IntervalPassive : Passive
{
    protected Timer timer = null;
    public abstract void SetTimer();
}

[System.Serializable]
public class GoldPassive : IntervalPassive
{
    public float interval;
    public float gold;

    public override void SetTimer()
    {
        timer = new Timer(interval, Use, isLooped: true);
        timer.Start();
    }

    public override void Use()
    {
        if (timer == null) { SetTimer(); }
        PlayerStats.SetGold(PlayerStats.Gold + gold);
    }
}