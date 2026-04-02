using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Projectile Data", menuName = "Scriptable Objects/Projectiles/Projectile")]
public class ProjectileData : ScriptableObject
{
    public GameObject prefab;
    public float speed;
    public bool isGuided;
    public Vector3 posOffset;
    [SerializeReference, SubclassSelector] public List<Effect> effects;
}
