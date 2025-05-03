using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Effects/Chills/ConstantChillsAction", fileName = "NewConstantChillsAction")]
public class ConstantChillsAction : AbstractChillsAction
{
    protected override int CalculateChills()
    {
        return 1 + CurrentChills;        
    }
}
