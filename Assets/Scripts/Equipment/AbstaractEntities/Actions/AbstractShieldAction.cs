using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractShieldAction : AbstractAction
{
    protected abstract int CalculateShield(int diceValue);

    public override void ApplyEffect(GameObject attacker, GameObject target, int diceValue)
    {
        int shield = CalculateShield(diceValue);
        if (attacker.TryGetComponent<HealthComponent>(out var health))
        {
            health.AddShield(shield);
        }
        else
        {
        }
    }
}
