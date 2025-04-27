using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pathogen/Definition")]
public class PathogenDefinition : ScriptableObject
{
    [SerializeField] private List<EquipmentCard> _equipment;
    [SerializeField] private string _pathogenName;
    [TextArea]
    [SerializeField] private string _description;
    [SerializeField] private float _maxHealth;

    public string PathogenName => _pathogenName;
    public string Description => _description;
    public float MaxHealth => _maxHealth;

    public IReadOnlyList<EquipmentCard> Equipment => _equipment;
}
