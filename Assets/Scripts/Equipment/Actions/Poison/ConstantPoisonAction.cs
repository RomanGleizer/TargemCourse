using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Effects/Poison/ConstantPoisonAction", fileName = "NewConstantPoisonAction")]
public class ConstantPoisonAction : AbstractPoisonAction
{
    [SerializeField] private int _poisonAmount;
    protected override int CalculatePoison()
    {
        return _poisonAmount + CurrentPoison;
    }
}
