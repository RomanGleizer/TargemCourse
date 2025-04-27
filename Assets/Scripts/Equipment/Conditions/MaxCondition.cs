using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxCondition : AbstractEquipmentCondition
{
    [SerializeField] int neededValue;
    public override bool IsSatisfied(int diceValue)
    {
        return diceValue <= neededValue;
    }

    public override void ChangeCondition(int diceValue)
    {
        Debug.Log($"EquipmentCard: Выпавшее значение {diceValue} больше необходимого.");
        return;
    }
}
