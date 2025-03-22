using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class PathogenController : MonoBehaviour
{
    [SerializeField] private PathogenDefinition definition;

    private HealthComponent _healthComponent;
    
    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
    }

    private void Start()
    {
        if (_healthComponent != null && definition != null)
        {
            _healthComponent.InitializeCurrentHealth(definition.MaxHealth);
        }
        else
        {
            Debug.LogError("Impossible initialize pathogen health: " +
                             $"{typeof(HealthComponent)} and {typeof(PathogenDefinition)} can't be null."
            );
        }
    }

    public void ApplyDamage(float damage)
    {
        _healthComponent?.TakeDamage(damage);
    }
}