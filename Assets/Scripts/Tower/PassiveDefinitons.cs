using FlexTimer;
using UnityEngine;

[System.Serializable]
public abstract class Passive
{
    public abstract void Use();
    public abstract Passive Clone();
}

public abstract class IntervalPassive : Passive
{
    protected Timer timer;
    protected abstract void SetTimer();
}

[System.Serializable]
public class GoldPassive : IntervalPassive
{
    public float interval;
    public float gold;

    protected override void SetTimer()
    {
        timer = new Timer(interval, Use, isLooped: true);
        timer.Start();
    }

    public override void Use()
    {
        if (timer == null) { SetTimer(); }
        PlayerStats.SetGold(PlayerStats.Gold + gold);
    }

    public override Passive Clone() => (Passive)this.MemberwiseClone();
}