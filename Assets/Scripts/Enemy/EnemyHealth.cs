using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private Image healthBar;

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
        healthBar.fillAmount = currentHealth / enemy.stats.health;
        if (currentHealth <= 0) { Die(); }
    }

    public void Die()
    {
        // throw new System.NotImplementedException();
    }
}
