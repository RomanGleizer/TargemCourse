using UnityEngine;

public abstract class AbstractDamageAction : ScriptableObject
{
    protected abstract int CalculateDamage(int diceValue);

    public void ApplyEffect(GameObject enemy, int diceValue)
    {
        int damage = CalculateDamage(diceValue);
        if (enemy.TryGetComponent<HealthComponent>(out var health))
        {
            health.TakeDamage(damage);
        }
        else
        {
        }
    }
}
