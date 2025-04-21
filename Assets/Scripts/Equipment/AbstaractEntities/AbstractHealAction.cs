using UnityEngine;

public abstract class AbstractHealAction : ScriptableObject
{
    protected abstract int CalculateHeal(int diceValue);

    public void ApplyEffect(GameObject target, int diceValue)
    {
        int heal = CalculateHeal(diceValue);
        //���� ������, ��� ��������� ��� �������� � �������� ����������!
        if (target.TryGetComponent<HealthComponent>(out var health))
        {
            health.AddHealth(heal);
        }
        else
        {
        }
    }
}
