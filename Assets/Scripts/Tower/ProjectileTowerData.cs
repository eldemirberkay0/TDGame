using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Tower Data", menuName = "Scriptable Objects/Towers/Projectile Tower")]
public class ProjectileTowerData : TowerData
{
    [Header("Projectile Tower Datas")]
    public float range;
    public float shootInterval;
    public float animTime;
    public ProjectileData projectileData;
}