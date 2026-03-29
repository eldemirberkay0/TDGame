using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Tower Data", menuName = "Scriptable Objects/Towers/Projectile Tower")]
public class ProjectileTowerData : TowerData
{
    [Header("Projectile Tower Datas")]
    public float range;
    public float shootInterval;
    public float animTime = 0.5f;
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public Vector3 projectilePosOffset;
    [SerializeReference, SubclassSelector] public List<Effect> effects;
}