using System;
using TMPro;
using UnityEngine;

public class EquipmentCard : MonoBehaviour
{
    [SerializeField] private EquipmentDefinition _definition;
    [SerializeField] private TextMeshProUGUI _textCondition;

    private int _remainingUses;

    public EquipmentDefinition Definition => _definition;
    public int RemainingUses => _remainingUses;

    void Start()
    {
        _remainingUses = _definition.UsageCount;
        UpdateConditionText();
    }

    public void UpdateConditionText()
    {
        _textCondition.text = _definition.Condition?.ConditionText ?? string.Empty;
    }

    public bool CanActivate(int diceValue)
    {
        if (_remainingUses <= 0) return false;
        var cond = _definition.Condition;
        return cond == null || cond.IsSatisfied(diceValue);
    }

    public void ActivateEquipment(GameObject attacker, GameObject target, int diceValue)
    {
        _definition.Effect.ApplyEffect(attacker, target, diceValue);
        _remainingUses--;
    }

    public void ResetUses()
    {
        _remainingUses = _definition.UsageCount;
    }
}
