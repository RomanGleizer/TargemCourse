using TMPro;
using UnityEngine;

public abstract class AbstractEquipmentCondition : ScriptableObject
{
    public string ConditionText;
    public abstract bool IsSatisfied(int diceValue);
    public abstract void ChangeCondition(int diceValue);
}
