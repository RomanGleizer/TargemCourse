using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Effects/Reroll/ReduceDiceValueRerollAction", fileName = "NewReduceDiceValueRerollAction")]
public class ReduceDiceValueRerollAction : AbstractRerollAction
{
    protected override void DoReroll(GameObject target, GameObject enemy, int diceValue)
    {
        if (target.TryGetComponent<DicePanel>(out DicePanel dicePanel))
        {
            dicePanel.AddDice(diceValue - 1);
        }
    }
}
