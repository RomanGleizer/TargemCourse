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
    public void UpdateInformation()
    {
        _text.text = _definition.Description ?? string.Empty;
        _name.text = _definition.Name ?? string.Empty;
        _condition.text = _definition.Condition?.ConditionText ?? string.Empty;
        _conditionImage.sprite = _definition.Condition.ConditionSprite;
        _equipImage.sprite = _definition.Sprite;
    }

    public bool CanActivate(int diceValue)
    {
        if (_remainingUses == 0)
            return false;

        var cond = _definition.Condition;
        return cond == null || cond.IsSatisfied(diceValue);
    }

    public void ActivateEquipment(GameObject attacker, GameObject target, Dice dice)
    {
        if (!CanActivate(dice.Value))
        {
            _definition.Condition.ChangeCondition(dice.Value);
            if (_definition.Condition is CountCondition countCondition)
            {
                _condition.text = _definition.Condition.ConditionText;
                Destroy(dice.gameObject);
                if (CanActivate(dice.Value))
                {
                    _definition.Condition.ResetCondition();

                    foreach (var effect in _definition.Effects)
                        effect.ApplyEffect(attacker, target, dice.Value);

                    if (_remainingUses > 0)
                    {
                        _remainingUses--;
                        if (_remainingUses == 0)
                        {
                            Destroy(gameObject);
                            return;
                        }
                    }
                }
                //можно ли тут сделать то, чтобы не было повторений?                
            }
            return;
        }

        Destroy(dice.gameObject);

        foreach (var effect in _definition.Effects)
            effect.ApplyEffect(attacker, target, dice.Value);

        if (_remainingUses > 0)
        {
            _remainingUses--;
            if (_remainingUses == 0)
            {
                Destroy(gameObject);
                return;
            }
        }

        UpdateInformation(); //нужно ли тут это?
    }

    public void ResetUses()
    {
        _remainingUses = _definition.UsageCount;
    }
}
