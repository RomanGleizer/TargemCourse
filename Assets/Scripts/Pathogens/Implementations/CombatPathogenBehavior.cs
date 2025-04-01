using UnityEngine;

public class CombatPathogenBehavior : IPathogenBehavior
{
    private ICombatAbility _combatAbility;

    public CombatPathogenBehavior(ICombatAbility combatAbility)
    {
        _combatAbility = combatAbility;
    }

    public void StartTurn()
    {
        Debug.Log("CombatBehavior: Начало боевого хода патогена.");
    }

    public void ExecuteAction(int diceValue)
    {
        if (_combatAbility.IsDiceValid(diceValue))
        {
            _combatAbility.Activate(diceValue);
            Debug.Log("CombatBehavior: Выполнена боeвая способность с кубиком " + diceValue);
        }
        else
        {
            Debug.Log("CombatBehavior: Выпавшее значение кубика " + diceValue + " не соответствует требованиям способности.");
        }
    }

    public void EndTurn()
    {
        Debug.Log("CombatBehavior: Конец боевого хода патогена.");
    }
}
