using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    private float _currentHealth;
    private float _currentShield;

    public event Action<float> OnHealthChanged;
    public event Action<float> OnShieldChanged;
    public event Action OnDeath;

    public float CurrentHealth => _currentHealth;
    public float CurrentShield => _currentShield;

    public void TakeDamage(float damage)
    {
        if (_currentShield >= damage)
        {
            _currentShield -= damage;
            OnShieldChanged?.Invoke(_currentShield);
        }
        else if (_currentShield > 0 && _currentShield < damage)
        {
            damage -= _currentShield;
            _currentShield = 0;
            OnShieldChanged?.Invoke(_currentShield);
            _currentHealth -= damage;
            OnHealthChanged?.Invoke(_currentHealth);
        }
        else if (_currentShield == 0)
        {
            _currentHealth -= damage;
            OnHealthChanged?.Invoke(_currentHealth);
        }

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void AddHealth(float heal)
    {
        _currentHealth += heal;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    public void AddShield(float shield)
    {
        _currentShield += shield;
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