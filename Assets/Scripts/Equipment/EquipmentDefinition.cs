using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Definition", fileName = "NewEquipmentDefinition")]
public class EquipmentDefinition : ScriptableObject
{
    [SerializeField] private string _name;
    [TextArea] [SerializeField] private string _description;
    [SerializeField] private int _usageCount;
    [SerializeField] private AbstractEquipmentCondition _condition;
    [SerializeField] private AbstractAction[] _effects;
    [SerializeField] private int _equipmentPower;
    [SerializeField] private Sprite _equipmentSprite;

    public string Name => _name;
    public string Description => _description;
    public int UsageCount => _usageCount;
    public AbstractEquipmentCondition Condition => _condition;
    public AbstractAction[] Effects => _effects;
    public Sprite Sprite => _equipmentSprite;
}