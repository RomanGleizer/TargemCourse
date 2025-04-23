using UnityEngine;

public abstract class AbstractRerollAction : ScriptableObject
{
    protected abstract void DoReroll(GameObject target, GameObject enemy, int diceValue);

    public void ApplyEffect(GameObject target, GameObject enemy, int diceValue)
    {
        DoReroll(target, enemy, diceValue);       
    }
}
