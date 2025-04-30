using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractChillsAction : AbstractAction
{
    protected abstract int CalculateChills();

    protected int CurrentChills;
    public override void ApplyEffect(GameObject attacker, GameObject target, int diceValue)
    {
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
