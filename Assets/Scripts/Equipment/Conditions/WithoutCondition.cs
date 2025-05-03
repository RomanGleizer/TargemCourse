using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Conditions/WithoutCondition", fileName = "NewWithoutCondition")]
public class WithoutCondition : AbstractEquipmentCondition
{
    public override bool IsSatisfied(int diceValue)
    {
        return true;
    }

    public override bool ChangeCondition(int diceValue)
    {
        Debug.Log($"�����-�� ������ ������, ��� �� ������ ���� :(");
        return false;
    }
}
