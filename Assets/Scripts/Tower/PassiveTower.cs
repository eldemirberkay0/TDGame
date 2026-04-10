using System.Collections.Generic;
using UnityEngine;
using FlexTimer;

public class PassiveTower : Tower
{
    protected PassiveTowerData towerData;

    protected void Awake()
    {
        towerData = TowerDatas[CurrentLevel - 1] as PassiveTowerData;
    }

    protected void OnEnable()
    {
        foreach (Passive passive in towerData.passives)
        {
            Passive passiveClone = passive.Clone();
            passiveClone.Use();
            Debug.Log(this + " used!"); passiveClone.Use();
        }
    }

    public override void SetTowerInfo(bool isActive)
    {
        Debug.Log("Not yet...");
    }

    public override void Upgrade()
    {
        CurrentLevel++;
        towerData = TowerDatas[CurrentLevel - 1] as PassiveTowerData;
    }
}
