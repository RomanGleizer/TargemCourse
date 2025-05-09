using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Conditions/NeedsCondition", fileName = "NewNeedsCondition")]
public class NeedsCondition : AbstractEquipmentCondition
{
    [SerializeField] int neededValue;
    public override bool IsSatisfied(int diceValue)
    {
        return diceValue == neededValue;
    }

    public override void ChangeCondition(int diceValue)
    {
        Debug.Log($"EquipmentCard: Выпавшее значение {diceValue} не равно необходимому.");
    }

    public override void ResetCondition()
    {
        return;
    }
}
