using UnityEngine;

public abstract class AbstractDamageAction : AbstractAction
{
    protected abstract int CalculateDamage(int diceValue);

    public override void ApplyEffect(GameObject attacker, GameObject target, int diceValue)
    {
        int damage = CalculateDamage(diceValue);
        if (target.TryGetComponent<HealthComponent>(out var health))
        {
            health.TakeDamage(damage);

            Debug.Log("Done damage " + damage);
            Debug.Log("Current health " + health.CurrentHealth);
        }
        else
        {
        }
    }
}
