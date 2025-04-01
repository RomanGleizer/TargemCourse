using UnityEngine;

[CreateAssetMenu(menuName = "Pathogen/Definition")]
public class PathogenDefinition : ScriptableObject 
{
    [SerializeField] private string _pathogenName;
    [SerializeField] private float _maxHealth;

    public string PathogenName => _pathogenName;
        
    public float MaxHealth => _maxHealth;
}