using UnityEngine;

public abstract class AbstractHeatAction : ScriptableObject
{
    protected abstract int CalculateHeat();

    protected int CurrentHeat;
    public void ApplyEffect(GameObject target)
    {
        //���� ������, ��� ��������� ��� �������� � �������� ����������!
        if (target.TryGetComponent<DicePanel>(out var dicePanel))
        {
            CurrentHeat = dicePanel.CurrentHeat;
            int heat = CalculateHeat();
            dicePanel.AddHeat(heat);
        }
        else
        {
        }
    }
}
