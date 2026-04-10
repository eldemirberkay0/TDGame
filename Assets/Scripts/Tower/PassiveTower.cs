using System.Collections.Generic;
using UnityEngine;
using FlexTimer;

public class PassiveTower : Tower
{
    protected PassiveTowerData towerData;
    protected List<Passive> currentPassives = new();

    protected void Awake()
    {
        towerData = TowerDatas[CurrentLevel - 1] as PassiveTowerData;
    }

    protected void OnEnable()
    {
        foreach (Passive passive in towerData.passives)
        {
            Passive passiveClone = passive.Clone();
            passiveClone.Use(this);
            currentPassives.Add(passiveClone);
            Debug.Log(this + " used!");
        }
    }

    public override void SetTowerInfo(bool isActive)
    {
        UIManager.Instance.SetTowerControlPanel(isActive);
    }

    public override void Upgrade()
    {
        CurrentLevel++;
        towerData = TowerDatas[CurrentLevel - 1] as PassiveTowerData;
        CancelPassives();
        foreach (Passive passive in towerData.passives)
        {
            Passive passiveClone = passive.Clone();
            passiveClone.Use(this);
            currentPassives.Add(passiveClone);
        }
    }

    public void CancelPassives()
    {
        foreach (Passive passive in currentPassives) { passive.Cancel(); }
        currentPassives.Clear();
    }
}
