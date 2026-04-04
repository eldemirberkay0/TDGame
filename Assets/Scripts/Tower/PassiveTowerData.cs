using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Passive Tower Data", menuName = "Scriptable Objects/Towers/Passive Tower")]
public class PassiveTowerData : TowerData
{
    [SerializeReference, SubclassSelector] public List<Passive> passives;
}
