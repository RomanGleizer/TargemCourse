using UnityEngine;

[CreateAssetMenu(menuName = "EquipmentCards/Definition", fileName = "NewEquipmentDefinition")]
public class EquipmentDefinition : ScriptableObject
{
    [SerializeField] private string _name;
    [TextArea] [SerializeField] private string _description;
    [SerializeField] int _usageCount;
    [SerializeField] AbstractEquipmentCondition _condition;
    [SerializeField] AbstractDamageAction _effect;

    public string Name => _name;
    public string Description => _description;
    public int UsageCount => _usageCount;
    public AbstractEquipmentCondition Condition => _condition;
    public AbstractDamageAction Effect => _effect;
}