using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPoisonAction : ScriptableObject
{
    protected abstract int CalculatePoison();

    protected int CurrentPoison;
    public void ApplyEffect(GameObject target)
    {        
        //Надо понять, как разделять своё здоровье и здоровье противника!
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
