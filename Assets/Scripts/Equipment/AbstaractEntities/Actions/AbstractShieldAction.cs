using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractShieldAction : ScriptableObject
{
    protected abstract int CalculateShield(int diceValue);

    public void ApplyEffect(GameObject target, int diceValue)
    {
        int shield = CalculateShield(diceValue);
        if (target.TryGetComponent<HealthComponent>(out var health))
        {
            health.AddShield(shield);
        }
        else
        {
        }
    }
}
