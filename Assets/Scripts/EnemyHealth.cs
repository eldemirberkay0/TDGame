using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] EnemyData enemyData;
    private float currentHealth;

    void Start()
    {
        currentHealth = enemyData.maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) { Die(); }
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }
}
