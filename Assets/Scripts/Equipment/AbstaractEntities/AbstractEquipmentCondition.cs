using TMPro;
using UnityEngine;

public abstract class AbstractEquipmentCondition : ScriptableObject
{
    public string ConditionText;
    public abstract bool IsSatisfied(int diceValue);
    public abstract bool ChangeCondition(int diceValue);

    public abstract void ResetCondition();
}
