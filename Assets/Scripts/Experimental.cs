[System.Serializable]
public abstract class EffectData
{
    protected Effect effect;
}

public abstract class TimerEffectData : EffectData
{
    public float duration;
}

[System.Serializable]
public abstract class SlowEffectData : EffectData
{
    public float slowPercent;
}