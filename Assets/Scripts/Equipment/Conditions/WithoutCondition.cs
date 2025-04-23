using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithoutCondition : AbstractEquipmentCondition
{
    public override bool IsSatisfied(int diceValue)
    {
        return true;
    }

    public override void ChangeCondition(int diceValue)
    {
        Debug.Log($"�����-�� ������ ������, ��� �� ������ ���� :(");
        return;
    }
}
