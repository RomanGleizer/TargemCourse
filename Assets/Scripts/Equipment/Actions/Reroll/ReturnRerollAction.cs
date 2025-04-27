using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Effects/Reroll/ReturnRerollAction", fileName = "NewReturnRerollAction")]
public class ReturnRerollAction : AbstractRerollAction
{
    [SerializeField] private int _damageAmount;
    protected override void DoReroll(GameObject target, GameObject enemy, int diceValue)
    {
        if (enemy.TryGetComponent<HealthComponent>(out HealthComponent health) &&
            target.TryGetComponent<DicePanel>(out DicePanel dicePanel))
        {
            health.TakeDamage(_damageAmount);
            dicePanel.AddDice();
        }
    }
}
