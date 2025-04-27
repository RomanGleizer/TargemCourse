using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Effects/Chills/ConstantChillsAction", fileName = "NewConstantChillsAction")]
public class ConstantChillsAction : AbstractHeatAction
{
    protected override int CalculateHeat()
    {
        return 1 + CurrentHeat;
    }
}
