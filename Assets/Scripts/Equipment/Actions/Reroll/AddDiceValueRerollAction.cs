using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Effects/Reroll/AddDiceValueRerollAction", fileName = "NewAddDiceValueRerollAction")]
public class AddDiceValueRerollAction : AbstractRerollAction
{
    protected override void DoReroll(GameObject target, GameObject enemy, int diceValue)
    {
        if (target.TryGetComponent<DicePanel>(out DicePanel dicePanel))
        {
                dicePanel.AddDice(diceValue + 1);            
        }
    }
}
