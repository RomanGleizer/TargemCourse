using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Effects/Reroll/DivideRerollAction", fileName = "NewDivideRerollAction")]
public class DivideRerollAction : AbstractRerollAction
{
    protected override void DoReroll(GameObject target, GameObject enemy, int diceValue)
    {
        if (target.TryGetComponent<DicePanel>(out DicePanel dicePanel))
        {
            for (int i = 0; i < 2; i++)
            {
                dicePanel.AddDice(1);
            }
        }
    }
}
