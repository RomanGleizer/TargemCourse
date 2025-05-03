using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Conditions/CountCondition", fileName = "NewCountCondition")]
public class CountCondition : AbstractEquipmentCondition
{
    [SerializeField] private int neededValue;
    public override bool IsSatisfied(int diceValue)
    {
        return neededValue <= 0;
    }

    public override bool ChangeCondition(int diceValue)
    {
        neededValue -= diceValue;
        ConditionText = neededValue.ToString();
        return true;
    }
}
