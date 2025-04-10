using UnityEngine;

[CreateAssetMenu(menuName = "EquipmentCards/Definition", fileName = "NewEquipmentDefinition")]
public class EquipmentDefinition : ScriptableObject
{
    [SerializeField] private string _name;
    [TextArea] [SerializeField] private string _description;
    [SerializeField] private EquipmentType _type;
    [SerializeField] int _usageCount;
    [SerializeField] AbstractEquipmentCondition _condition;

    public string Name => _name;
    public string Description => _description;
    public EquipmentType Type => _type;
    public int UsageCount => _usageCount;
    public AbstractEquipmentCondition Condition => _condition;
}