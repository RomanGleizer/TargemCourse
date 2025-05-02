using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pathogen/Definition")]
public class PathogenDefinition : ScriptableObject
{
    [SerializeField] private List<EquipmentDefinition> _equipmentDefinitions;
    [SerializeField] private string _pathogenName;
    [TextArea][SerializeField] private string _description;

    public string PathogenName => _pathogenName;
    public string Description => _description;
    public IReadOnlyList<EquipmentDefinition> EquipmentDefinitions => _equipmentDefinitions;
}