using UnityEngine;

public class NonCombatPathogenBehavior : IPathogenBehavior
{
    public void StartTurn()
    {
        Debug.Log("NonCombatBehavior: ������ ��������� ���� (����������� �� �����).");
    }

    public void ExecuteAction(int diceValue)
    {
        Debug.Log("NonCombatBehavior: ����������� �������� ����������� �� �����. ����� (" + diceValue + ") �� ������������ ��� ������ ������������.");
    }

    public void EndTurn()
    {
        Debug.Log("NonCombatBehavior: ����� ��������� ���� ��������.");
    }
}
