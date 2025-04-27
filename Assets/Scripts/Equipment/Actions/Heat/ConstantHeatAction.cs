using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Effects/Heat/ConstantHeatAction", fileName = "NewConstantHeatAction")]
public class ConstantHeatAction : AbstractHeatAction
{
    protected override int CalculateHeat()
    {
        return 1 + CurrentHeat;
    }
}
