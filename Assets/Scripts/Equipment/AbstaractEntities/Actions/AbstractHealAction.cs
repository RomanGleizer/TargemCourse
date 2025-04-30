using UnityEngine;

public abstract class AbstractHealAction : AbstractAction
{
    protected abstract int CalculateHeal(int diceValue);

    public override void ApplyEffect(GameObject attacker, GameObject target, int diceValue)
    {
        int heal = CalculateHeal(diceValue);
        if (attacker.TryGetComponent<HealthComponent>(out var health))
        {
            health.AddHealth(heal);
        }
        else
        {
        }
    }
}
