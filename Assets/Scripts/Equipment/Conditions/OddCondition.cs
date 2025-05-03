using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Conditions/OddCondition", fileName = "NewOddCondition")]
public class OddCondition : AbstractEquipmentCondition
{
    public override bool IsSatisfied(int diceValue)
    {
        return diceValue % 2 != 0;
    }

    public override bool ChangeCondition(int diceValue)
    {
        Debug.Log($"EquipmentCard: Выпавшее значение {diceValue} четное.");
        return false;
    }

    public override void ResetCondition()
    {
        return;
    }
}
