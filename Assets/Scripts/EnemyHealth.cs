using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private Enemy _enemy;
    private float _currentHealth;

    void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    void Start()
    {
        _currentHealth = _enemy.Stats.MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0) { Die(); }
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }
}
