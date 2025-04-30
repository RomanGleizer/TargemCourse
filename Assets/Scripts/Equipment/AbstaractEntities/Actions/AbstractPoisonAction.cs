using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPoisonAction : AbstractAction
{
    protected abstract int CalculatePoison();

    protected int CurrentPoison;
    public override void ApplyEffect(GameObject attacker, GameObject target, int diceValue)
    {        
        if (target.TryGetComponent<HealthComponent>(out var health))
        {
            CurrentPoison = health.CurrentPoison;
            int poison = CalculatePoison();
            health.AddPoison(poison);
        }
        else
        {
        }
    }
}
