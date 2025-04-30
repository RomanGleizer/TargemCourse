using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Definition", fileName = "NewEquipmentDefinition")]
public class EquipmentDefinition : ScriptableObject
{
    [SerializeField] private string _name;
    [TextArea] [SerializeField] private string _description;
    [SerializeField] private int _usageCount;
    [SerializeField] private AbstractEquipmentCondition _condition;
    [SerializeField] private AbstractAction _effect;
    [SerializeField] private int _equipmentPower;

    public string Name => _name;
    public string Description => _description;
    public int UsageCount => _usageCount;
    public AbstractEquipmentCondition Condition => _condition;
    public AbstractAction Effect => _effect;
}