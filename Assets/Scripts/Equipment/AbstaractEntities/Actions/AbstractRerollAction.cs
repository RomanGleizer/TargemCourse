using UnityEngine;

public abstract class AbstractRerollAction : AbstractAction
{
    protected abstract void DoReroll(GameObject target, GameObject enemy, int diceValue);

    public override void ApplyEffect(GameObject attacker, GameObject target, int diceValue)
    {
        DoReroll(attacker, target, diceValue);       
    }
}
