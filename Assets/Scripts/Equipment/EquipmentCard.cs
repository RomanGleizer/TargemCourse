using TMPro;
using UnityEngine;

public class EquipmentCard : MonoBehaviour
{
    [SerializeField] private EquipmentDefinition _definition;
    [SerializeField] private TextMeshProUGUI _textCondition;

    private int _remainingUses;
    private AbstractEquipmentCondition _runtimeCondition;

    public EquipmentDefinition Definition => _definition;
    public int RemainingUses => _remainingUses;

    public void Initialize(EquipmentDefinition definition)
    {
        _definition = definition;
        _runtimeCondition = _definition.Condition != null
        ? ScriptableObject.Instantiate(_definition.Condition)
        : null;

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
        _textCondition.text = _runtimeCondition != null ? _runtimeCondition.ConditionText : null ?? string.Empty;
    }

    public bool CanActivate(int diceValue)
    {
        if (_remainingUses <= 0) return false;
        var cond = _runtimeCondition;
        return cond == null || cond.IsSatisfied(diceValue);
    }

    public void ActivateEquipment(GameObject attacker, GameObject target, Dice dice)
    {
        var cond = _runtimeCondition;
        if (!CanActivate(dice.Value))
        {
            if (cond.ChangeCondition(dice.Value))
            {
                _textCondition.text = cond.ConditionText;
                Destroy(dice.gameObject);
            };

            if (!CanActivate(dice.Value)) return;
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