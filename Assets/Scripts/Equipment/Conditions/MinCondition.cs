using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Conditions/MinCondition", fileName = "NewMinCondition")]
public class MinCondition : AbstractEquipmentCondition
{
    [SerializeField] int neededValue;
    public override bool IsSatisfied(int diceValue)
    {
        return diceValue >= neededValue;
    }

    public override void ChangeCondition(int diceValue)
    {
        Debug.Log($"EquipmentCard: Выпавшее значение {diceValue} меньше необходимого.");
        return;
    }
}
