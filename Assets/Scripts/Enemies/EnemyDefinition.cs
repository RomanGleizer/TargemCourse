using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Definition")]
public class EnemyDefinition : ScriptableObject
{
    [SerializeField] private List<EquipmentDefinition> _equipmentDefinitions;
    [SerializeField] private string _enemyName;
    [TextArea][SerializeField] private string _description;
    [SerializeField] private Sprite _enemySprite;

    public string EnemyName => _enemyName;
    public string Description => _description;
    public Sprite EnemySprite => _enemySprite;
    public IReadOnlyList<EquipmentDefinition> EquipmentDefinitions => _equipmentDefinitions;
}
