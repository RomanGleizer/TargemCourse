using UnityEngine;

public class PathogenController : MonoBehaviour
{
    [SerializeField] private PathogenDefinition definition;

    private HealthComponent _healthComponent;
    private IPathogenBehavior _pathogenBehavior;
    private ICombatAbility _combatAbility;
    private IPathogenMovement _pathogenMovement;
    
    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        
        _pathogenBehavior = new BasicPathogenBehavior();
        _combatAbility = new BasicCombatAbility();
        _pathogenMovement = new BasicPathogenMovement();
    }

    private void Start()
    {
        if (definition != null && _healthComponent != null)
        {
            _healthComponent.InitializeCurrentHealthOnStart(definition.MaxHealth);
        }
        else
        {
            Debug.LogError("Impossible initialize pathogen health: " +
                           $"{typeof(HealthComponent)} and {typeof(PathogenDefinition)} can't be null."
            );
        }

        _pathogenBehavior.StartTurn();
    }

    /// <summary>
    /// Выполнение действия патогена с учетом значения кубика
    /// </summary>
    /// <param name="diceValue">Выпавшее значение кубика</param>
    public void PerformAction(int diceValue)
    {
        if (_combatAbility.IsDiceValid(diceValue))
        {
            _pathogenBehavior.ExecuteAction(diceValue);
        }
        else
        {
            Debug.Log("Выпавшее значение кубика " + diceValue + " не соответствует требованиям способности.");
        }
    }

    /// <summary>
    /// Перемещает патоген к указанному узлу графа
    /// </summary>
    /// <param name="targetNode">Целевой узел</param>
    public void MoveToNode(Node targetNode)
    {
        _pathogenMovement.MoveTo(targetNode);
    }

    /// <summary>
    /// Возвращает текущий узел, где находится патоген
    /// </summary>
    public Node GetCurrentNode()
    {
        return _pathogenMovement.GetCurrentNode();
    }

    /// <summary>
    /// Завершает ход патогена
    /// </summary>
    public void EndTurn()
    {
        _pathogenBehavior.EndTurn();
    }
}
