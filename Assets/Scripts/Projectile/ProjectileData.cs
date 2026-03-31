using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Projectile Data", menuName = "Scriptable Objects/Projectiles/Projectile")]
public class ProjectileData : ScriptableObject
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public bool isProjectileGuided;
    public Vector3 projectilePosOffset;
    [SerializeReference, SubclassSelector] public List<Effect> effects;
}
