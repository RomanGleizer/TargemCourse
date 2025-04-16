using UnityEngine;

public abstract class AbstractEquipmentCondition : ScriptableObject
{
    public abstract bool IsSatisfied(int diceValue);
}
