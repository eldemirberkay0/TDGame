using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Data", menuName = "Scriptable Objects/Projectiles/Projectile")]
public class ProjectileData : ScriptableObject
{
    public GameObject prefab;
    public float speed;
    public bool isGuided;
    public Vector3 posOffset;

    [Header("Optionals")]
    public float effectRadius;
    public GameObject hitVisual;
}
