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

    /// <summary>
    /// Выполнение действия патогена с учетом значения кубика.
    /// В боевом режиме поведение обрабатывает проверку и активацию боевой способности,
    /// в небоевом – запускает логику перемещения или иной соответствующий функционал.
    /// </summary>
    /// <param name="diceValue">Выпавшее значение кубика</param>
    public void PerformAction(int diceValue)
    {
        _pathogenBehavior.ExecuteAction(diceValue);
    }

    /// <summary>
    /// Перемещает патогена к указанному узлу графа.
    /// </summary>
    /// <param name="targetNode">Целевой узел</param>
    public void MoveToNode(Node targetNode)
    {
        _pathogenMovement.MoveTo(targetNode);
    }

    /// <summary>
    /// Возвращает текущий узел, где находится патоген.
    /// </summary>
    public Node GetCurrentNode()
    {
        return _pathogenMovement.GetCurrentNode();
    }

    /// <summary>
    /// Завершает ход патогена.
    /// </summary>
    public void EndTurn()
    {
        _pathogenBehavior.EndTurn();
    }

    /// <summary>
    /// Позволяет динамически переключать поведение патогена.
    /// Например, при переходе с карты в бой можно вызвать этот метод и установить боевое поведение.
    /// </summary>
    /// <param name="newBehavior">Новая реализация IPathogenBehavior</param>
    public void SetBehavior(IPathogenBehavior newBehavior)
    {
        _pathogenBehavior = newBehavior;
        _pathogenBehavior.StartTurn();
    }
}
