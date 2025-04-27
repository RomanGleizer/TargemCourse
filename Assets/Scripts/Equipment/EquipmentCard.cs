using System;
using TMPro;
using UnityEngine;

public class EquipmentCard : MonoBehaviour
{
    [SerializeField] private EquipmentDefinition equipmentDefinition;
    [SerializeField] private TextMeshProUGUI _textCondition;

    public EquipmentDefinition EquipmentDefinition => equipmentDefinition;

    void Start()
    {
        UpdateConditionText();
    }

    public void UpdateConditionText()
    {
        _textCondition.text = EquipmentDefinition.Condition.ConditionText;
    }

    public void ActivateEquipment(GameObject target, int diceValue)
    {
        if (equipmentDefinition.Condition == null || equipmentDefinition.Condition.IsSatisfied(diceValue))
        {
            equipmentDefinition.Effect.ApplyEffect(target, diceValue);
        }
        else
        {
            equipmentDefinition.Condition.ChangeCondition(diceValue);            
        }
    }

    public void InitializeEquipmentDefinitionOnStart()
    {

    }
}