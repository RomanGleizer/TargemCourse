using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Effects/Reroll/DivideRerollAction", fileName = "NewDivideRerollAction")]
public class DivideRerollAction : AbstractRerollAction
{
    protected override void DoReroll(GameObject target, GameObject enemy, int diceValue)
    {
        if (target.TryGetComponent<PathogenController>(out PathogenController pathogen))
        {
            for (int i = 0; i < 2; i++)
            {
                pathogen.DicePanel.AddDice(1);
            }
        }
        
        if (target.TryGetComponent<EnemyController>(out EnemyController enemyCon))
        {
            for (int i = 0; i < 2; i++)
            {
                enemyCon.DicePanel.AddDice(1);
            }
        }
    }
}
