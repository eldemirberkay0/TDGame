using System.Collections.Generic;
using UnityEngine;
using FlexTimer;

public class PassiveTower : Tower
{
    protected new PassiveTowerData towerData => base.towerData as PassiveTowerData;

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
}
