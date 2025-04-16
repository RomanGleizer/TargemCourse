using UnityEngine;

public class PathogenController : MonoBehaviour
{
    [SerializeField] private PathogenDefinition definition;
    [SerializeField] private bool isCombatMode = false;

    private HealthComponent _healthComponent;
    private IPathogenBehavior _pathogenBehavior;
    private ICombatAbility _combatAbility;
    private IPathogenMovement _pathogenMovement;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();

        _combatAbility = new CombatAbility();
        _pathogenMovement = new PathogenMovement();

        if (isCombatMode)
        {
            _pathogenBehavior = new CombatPathogenBehavior(_combatAbility);
        }
        else
        {
            _pathogenBehavior = new NonCombatPathogenBehavior();
        }
    }

    private void Start()
    {
        if (definition != null && _healthComponent != null)
        {
            _healthComponent.InitializeCurrentHealthOnStart(definition.MaxHealth);
        }
        else
        {
            Debug.LogError("Невозможно инициализировать здоровье патогена: компоненты HealthComponent и PathogenDefinition не могут быть null.");
        }

        _pathogenBehavior.StartTurn();
    }

    public void PerformAction(int diceValue)
    {
        _pathogenBehavior.ExecuteAction(diceValue);
    }

    public void MoveToNode(Node targetNode)
    {
        _pathogenMovement.MoveTo(targetNode);
    }

    public Node GetCurrentNode()
    {
        return _pathogenMovement.GetCurrentNode();
    }

    public void EndTurn()
    {
        _pathogenBehavior.EndTurn();
    }

    public void SetBehavior(IPathogenBehavior newBehavior)
    {
        _pathogenBehavior = newBehavior;
        _pathogenBehavior.StartTurn();
    }
}
