using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public const int LAYER = 7;

    public EnemyData stats;
    public EnemyHealth Health { get; private set; }
    public EnemyController Controller { get; private set; }
    public EnemyEffectHandler EffectHandler { get; private set; }
    public SpriteRenderer Renderer { get; private set; }

    private void Awake()
    {
        Controller = GetComponent<EnemyController>();
        Health = GetComponent<EnemyHealth>();
        EffectHandler = GetComponent<EnemyEffectHandler>();
        Renderer = GetComponent<SpriteRenderer>();
    }
}
