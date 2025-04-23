using TMPro;
using UnityEngine;

public class EquipmentCard : MonoBehaviour
{
    [SerializeField] private EquipmentDefinition equipmentDefinition;
    [SerializeField] private TextMeshProUGUI _textCondition;

    private int _diceValue;

    public EquipmentDefinition EquipmentDefinition => equipmentDefinition;

    void Start()
    {
        UpdateConditionText();
    }

    public void UpdateConditionText()
    {
        _textCondition.text = EquipmentDefinition.Condition.ConditionText;
    }

    public void ActivateEquipment(GameObject target)
    {
        if (equipmentDefinition.Condition == null || equipmentDefinition.Condition.IsSatisfied(_diceValue))
        {
            equipmentDefinition.Effect.ApplyEffect(target, _diceValue);
        }
        else
        {
            equipmentDefinition.Condition.ChangeCondition(_diceValue);            
        }
    }

    public void InitializeEquipmentDefinitionOnStart()
    {

    }
}