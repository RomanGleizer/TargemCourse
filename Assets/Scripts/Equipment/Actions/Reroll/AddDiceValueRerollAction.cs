using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Effects/Reroll/AddDiceValueRerollAction", fileName = "NewAddDiceValueRerollAction")]
public class AddDiceValueRerollAction : AbstractRerollAction
{
    protected override void DoReroll(GameObject target, GameObject enemy, int diceValue)
    {        
        if (target.TryGetComponent<PathogenController>(out PathogenController pathogen))
        {
            pathogen.DicePanel.AddDice(diceValue + 1);
        }
        
        if (target.TryGetComponent<EnemyController>(out EnemyController enemyCon))
        {
            enemyCon.DicePanel.AddDice(diceValue + 1);
        }
    }
}
