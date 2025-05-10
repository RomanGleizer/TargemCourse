using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Effects/Reroll/ThrowOverRerollAction", fileName = "NewThrowOverRerollAction")]
public class ThrowOverRerollAction : AbstractRerollAction
{
    protected override void DoReroll(GameObject target, GameObject enemy, int diceValue)
    {
        if (target.TryGetComponent<PathogenController>(out PathogenController pathogen))
        {
            pathogen.DicePanel.AddDice();
        }
        
        if (target.TryGetComponent<EnemyController>(out EnemyController enemyCon))
        {
            enemyCon.DicePanel.AddDice();
        }
    }
}
