using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    private float _currentHealth;
    private float _currentShield;
    private int _currentPoison;

    public event Action<float> OnHealthChanged;
    public event Action<float> OnShieldChanged;
    public event Action<int> OnPoisonChanged;
    public event Action OnDeath;

    public float CurrentHealth => _currentHealth;
    public float CurrentShield => _currentShield;
    public int CurrentPoison => _currentPoison;
    public float MaxHealth => _maxHealth;
    
    public void InitializeHealth()
    {
        _currentHealth = _maxHealth;
        _currentShield = 0;
        _currentPoison = 0;
        OnHealthChanged?.Invoke(_currentHealth);
        OnShieldChanged?.Invoke(_currentShield);
        OnPoisonChanged?.Invoke(_currentPoison);
    }

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
            if (_currentHealth < 0f) _currentHealth = 0f;
            OnHealthChanged?.Invoke(_currentHealth);
        }
        else if (_currentShield == 0)
        {
            _currentHealth -= damage;
            if (_currentHealth < 0f) _currentHealth = 0f;
            OnHealthChanged?.Invoke(_currentHealth);
        }

        if (_currentHealth <= 0f)
            Die();
    }

    //Метод должен вызываться в начале хода
    public void TakeDamageByPoison(float poison)
    {
        _currentHealth -= poison;
        if (_currentHealth < 0f) _currentHealth = 0f;
        OnHealthChanged?.Invoke(_currentHealth);

        _currentPoison = Mathf.Max(0, _currentPoison - 1);
        OnPoisonChanged?.Invoke(_currentPoison);

        if (_currentHealth <= 0f)
            Die();
    }

    public void AddHealth(float heal)
    {
        _currentHealth += heal;
        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;

        OnHealthChanged?.Invoke(_currentHealth);
    }

    public void AddShield(float shield)
    {
        _currentShield += shield;
        OnShieldChanged?.Invoke(_currentShield);
    }

    public void AddPoison(int poison)
    {
        _currentPoison = poison;
        OnPoisonChanged?.Invoke(_currentPoison);
    }

    private void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}