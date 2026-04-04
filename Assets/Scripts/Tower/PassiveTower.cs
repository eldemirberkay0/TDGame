using UnityEngine;

public class PassiveTower : Tower<PassiveTowerData>
{
    void Start()
    {
        foreach (Passive passive in towerData.passives) { passive.Use(); }
    }
}
