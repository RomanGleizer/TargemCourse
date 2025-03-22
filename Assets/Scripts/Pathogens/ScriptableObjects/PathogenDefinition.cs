using UnityEngine;

[CreateAssetMenu(menuName = "Pathogen/Definition")]
public class PathogenDefinition : ScriptableObject 
{
    private string _pathogenName;
    private float _maxHealth;

    public string PathogenName => _pathogenName;
        
    public float MaxHealth => _maxHealth;
}