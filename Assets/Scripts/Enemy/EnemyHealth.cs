using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private Image healthBar;

    private Enemy enemy;
    private float currentHealth;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        currentHealth = enemy.stats.health;
        healthBar.fillAmount = currentHealth / enemy.stats.health;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / enemy.stats.health;
        if (currentHealth <= 0) { Die(); }
    }

    public void Die()
    {
        PlayerStats.SetGold(PlayerStats.Gold + enemy.stats.reward);
        WaveManager.Instance.UpdateEnemyCounter();
        enemy.EffectHandler.CurrentEffects.Clear();
        gameObject.SetActive(false);
    }
}
