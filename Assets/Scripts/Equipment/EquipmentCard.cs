using TMPro;
using UnityEngine;

public class EquipmentCard : MonoBehaviour
{
    [SerializeField] private EquipmentDefinition _definition;
    [SerializeField] private TextMeshProUGUI _textCondition;

    private int _remainingUses;

    public EquipmentDefinition Definition => _definition;
    public int RemainingUses => _remainingUses;

    public void Initialize(EquipmentDefinition definition)
    {
        _definition = definition;

        ResetUses();
        UpdateConditionText();
    }

    void Start()
    {
        // Если карточка создана из инспектора, сбросим состояния
        if (_remainingUses == 0)
            ResetUses();
        UpdateConditionText();
    }

    public void UpdateConditionText()
    {
        _textCondition.text = _definition.Condition != null ? _definition.Condition.ConditionText : null ?? string.Empty;
    }

    public bool CanActivate(int diceValue)
    {
        if (_remainingUses <= 0) return false;
        var cond = _definition.Condition;
        return cond == null || cond.IsSatisfied(diceValue);
    }

    public void ActivateEquipment(GameObject attacker, GameObject target, Dice dice)
    {
        if (!CanActivate(dice.Value))
        {
            if (_definition.Condition.ChangeCondition(dice.Value))
            {
                _textCondition.text = _definition.Condition.ConditionText;
                Destroy(dice.gameObject);
            };

            if (!CanActivate(dice.Value)) return;

            _definition.Condition.ResetCondition();
        };

        Destroy(dice.gameObject);
        foreach (var effect in _definition.Effects)
            effect.ApplyEffect(attacker, target, dice.Value);

        _remainingUses--;
        if (_remainingUses <= 0)
            Destroy(gameObject);
        else
            UpdateConditionText();
    }

    public void ResetUses()
    {
        _remainingUses = _definition.UsageCount;
    }
}