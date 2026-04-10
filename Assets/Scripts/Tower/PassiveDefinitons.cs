using FlexTimer;
using TMPro;
using UnityEngine;

[System.Serializable]
public abstract class Passive
{
    public abstract void Use(Tower tower);
    public abstract void Cancel();
    public Passive Clone() => (Passive)this.MemberwiseClone();
}

public abstract class IntervalPassive : Passive
{
    protected Timer timer = null;
    protected abstract void SetTimer();
}

[System.Serializable]
public class GoldPassive : IntervalPassive
{
    [SerializeField] protected float interval;
    [SerializeField] protected float gold;
    [SerializeField] protected GameObject effectPrefab;
    protected Tower tower;

    protected override void SetTimer()
    {
        timer = new Timer(interval, () => Use(tower), isLooped: true);
        timer.Start();
    }

    public override void Use(Tower tower)
    {
        if (timer == null)
        {
            this.tower = tower;
            SetTimer();
        }
        PlayerStats.SetGold(PlayerStats.Gold + gold);
        GameObject effect = Object.Instantiate(effectPrefab, tower.transform.position, tower.transform.rotation);
        Debug.Log(tower.transform.position);
        Debug.Log(effect.transform.position);
        effect.SetActive(true);
        effect.GetComponentInChildren<Animator>().SetTrigger("ShouldReward");
        effect.GetComponentInChildren<TMP_Text>().text = "+  " + gold.ToString();
        TimerManager.RegisterEvent(2f, () => Object.Destroy(effect.gameObject));
    }

    public override void Cancel()
    {
        timer.Cancel();
        timer = null;
    }
}