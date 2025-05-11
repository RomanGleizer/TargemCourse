using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentCard : MonoBehaviour
{
    [SerializeField] private EquipmentDefinition _definition;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _condition;
    [SerializeField] private Image _equipImage;
    [SerializeField] private Image _conditionImage;

    private int _remainingUses;

    public EquipmentDefinition Definition => _definition;

    public int RemainingUses => _remainingUses;

    public void Initialize(EquipmentDefinition definition)
    {
        _definition = definition;
        ResetUses();
        UpdateInformation();
    }

    void Start()
    {
        UpdateInformation();
    }

    public void SetRemainingUses(int uses)
    {
        _remainingUses = uses;
        UpdateInformation();
    }

    private void UpdateInformation()
    {
        _text.text = _definition.Description ?? string.Empty;
        _name.text = _definition.Name ?? string.Empty;
        _condition.text = _definition.Condition?.ConditionText ?? string.Empty;
        _conditionImage.sprite = _definition.Condition?.ConditionSprite;
        _equipImage.sprite = _definition.Sprite;
    }

    public bool CanActivate(int diceValue)
    {
        if (_remainingUses == 0)
            return false;

        var cond = _definition.Condition;
        return cond == null || cond.IsSatisfied(diceValue);
    }

    public bool ActivateEquipment(GameObject attacker, GameObject target, Dice dice)
    {
        int val = dice.Value;

        if (!CanActivate(val))
        {
            _definition.Condition?.ChangeCondition(val);
            UpdateInformation();

            if (_definition.Condition is CountCondition)
                Destroy(dice.gameObject);

            return false;
        }

        Destroy(dice.gameObject);

        foreach (var effect in _definition.Effects)
            effect.ApplyEffect(attacker, target, val);

        if (_definition.Condition is CountCondition)
            _definition.Condition.ResetCondition();

        if (_remainingUses > 0)
        {
            _remainingUses--;
            if (_remainingUses == 0)
            {
                Destroy(gameObject);
                return true;
            }
        }

        UpdateInformation();
        return true;
    }

    public void ResetUses()
    {
        _remainingUses = _definition.UsageCount;
        UpdateInformation();
    }
}
