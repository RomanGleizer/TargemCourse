using TMPro;
using UnityEngine;

public abstract class AbstractEquipmentCondition : ScriptableObject
{
    public string ConditionText;
    public Sprite ConditionSprite;
    public abstract bool IsSatisfied(int diceValue);
    public abstract void ChangeCondition(int diceValue);

    public abstract void ResetCondition();
}
