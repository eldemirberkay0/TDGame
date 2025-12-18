using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Tower Data", menuName = "Scriptable Objects/Towers/Projectile Tower")]
public class ProjectileTowerData : TowerData
{
    [field: Header("Projectile Tower Datas")]
    [field: SerializeField] public float Range { get; private set; }
    [field: SerializeField]public GameObject ProjectilePrefab { get; private set; }
    [field: SerializeField] public float FireRate { get; private set; }
    [field: SerializeReference, SerializeField] public List<IEffect> Effects { get; private set; }

    [ContextMenu("Add Damage Effect")] public void AddDamage() { Effects.Add(new DamageEffect()); }
}
