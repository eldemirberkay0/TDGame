using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Projectile Tower Data", menuName = "Scriptable Objects/Towers/Projectile Tower")]
public class ProjectileTowerData : TowerData
{
    [Header("Projectile Tower Datas")]
    public float range;
    public float shootInterval;
    [SerializeReference, SubclassSelector] public List<Effect> effects;
    public ProjectileData projectileData;
}