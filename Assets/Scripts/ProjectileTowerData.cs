using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Tower Data", menuName = "Scriptable Objects/Towers/Projectile Tower")]
public class ProjectileTowerData : TowerData
{
    [Header("Projectile Tower Datas")]
    public float range;
    public float shootInterval;
    public GameObject projectilePrefab;
    public float projectileSpeed;
    [SerializeReference] public List<IEffect> effects;

    [ContextMenu("Add Damage Effect")] private void AddDamage() { effects.Add(new DamageEffect()); }
}