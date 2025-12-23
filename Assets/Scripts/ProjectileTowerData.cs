using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Tower Data", menuName = "Scriptable Objects/Towers/Projectile Tower")]
public class ProjectileTowerData : TowerData
{
    [Header("Projectile Tower Datas")]
    public float range;
    public GameObject projectilePrefab;
    public float fireRate;
    [SerializeReference] public List<IEffect> Effects;

    [ContextMenu("Add Damage Effect")] public void AddDamage() { Effects.Add(new DamageEffect()); }
}
