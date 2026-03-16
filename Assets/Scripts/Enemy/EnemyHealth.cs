using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private Enemy enemy;
    private float currentHealth;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void Start()
    {
        currentHealth = enemy.stats.health;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0) { Die(); }
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }
}
