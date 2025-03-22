using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour 
{
    private float _currentHealth;

    public event Action<float> OnHealthChanged;
    public event Action OnDeath;

    public void TakeDamage(float damage) 
    {
        _currentHealth -= damage;
        OnHealthChanged?.Invoke(_currentHealth);
        
        if (_currentHealth <= 0) 
        {
            Die();
        }
    }

    private void Die() 
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    public void InitializeCurrentHealthOnStart(float health)
    {
        _currentHealth = health;
    }
}