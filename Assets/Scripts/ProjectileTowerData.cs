using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Tower Data", menuName = "Scriptable Objects/Towers/Projectile Tower")]
public class ProjectileTowerData : TowerData
{
    [Header("Projectile Tower Datas")]
    public GameObject projectile;
    public float fireRate;
    [SerializeReference] public List<IEffect> effects;

    [ContextMenu("Add Damage Effect")] public void AddDamage() { effects.Add(new DamageEffect()); }
}
