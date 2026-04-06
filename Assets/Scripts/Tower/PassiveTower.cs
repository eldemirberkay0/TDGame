using System.Collections.Generic;
using UnityEngine;
using FlexTimer;

public class PassiveTower : Tower<PassiveTowerData>
{
    private void OnEnable()
    {
        foreach (Passive passive in towerData.passives)
        {
            Passive passiveClone = passive.Clone();
            passiveClone.Use();
            Debug.Log(this + " used!"); passiveClone.Use();
        }
    }
}
