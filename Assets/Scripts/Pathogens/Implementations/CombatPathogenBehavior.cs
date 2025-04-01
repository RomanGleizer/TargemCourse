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
        Debug.Log("CombatBehavior: ������ ������� ���� ��������.");
    }

    public void ExecuteAction(int diceValue)
    {
        if (_combatAbility.IsDiceValid(diceValue))
        {
            _combatAbility.Activate(diceValue);
            Debug.Log("CombatBehavior: ��������� ��e��� ����������� � ������� " + diceValue);
        }
        else
        {
            Debug.Log("CombatBehavior: �������� �������� ������ " + diceValue + " �� ������������� ����������� �����������.");
        }
    }

    public void EndTurn()
    {
        Debug.Log("CombatBehavior: ����� ������� ���� ��������.");
    }
}
