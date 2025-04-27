using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Effects/Poison/DoublePoisonAction", fileName = "NewDoublePoisonAction")]
public class DoublePoisonAction : AbstractPoisonAction
{
    private int _currentPoisonAmount;
    protected override int CalculatePoison()
    {
        _currentPoisonAmount = CurrentPoison;
        return _currentPoisonAmount * 2;
    }
}
