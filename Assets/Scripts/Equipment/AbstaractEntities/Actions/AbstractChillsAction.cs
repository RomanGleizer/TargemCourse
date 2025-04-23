using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractChillsAction : ScriptableObject
{
    protected abstract int CalculateChills();

    protected int CurrentChills;
    public void ApplyEffect(GameObject target)
    {
        //Надо понять, как разделять своё здоровье и здоровье противника!
        if (target.TryGetComponent<DicePanel>(out var dicePanel))
        {
            CurrentChills = dicePanel.CurrentChills;
            int chills = CalculateChills();
            dicePanel.AddChills(chills);
        }
        else
        {
        }
    }
}
