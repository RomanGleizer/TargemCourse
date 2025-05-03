using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class PathogenController : MonoBehaviour
{
    [SerializeField] private PathogenDefinition _definition;
    [SerializeField] private Transform _equipmentPanel;

    [SerializeField] private DicePanel _dicePanel;
    [SerializeField] private DiceDefinition _diceDefinition;

    [SerializeField] private CardPanel _cardPanel;

    private HealthComponent _healthComponent;
    private EnemyController _enemyController;

    public DicePanel DicePanel => _dicePanel;
    public CardPanel CardPanel => _cardPanel;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _enemyController = FindObjectOfType<EnemyController>();
    }

    private void Start()
    {
        _healthComponent.InitializeHealth();
    }

    private void SpawnEquipment()
    {
        foreach (var eqDef in _definition.EquipmentDefinitions)
        {
            _cardPanel.AddEquipment(eqDef);
        }
    }

    public bool TryActivateEquipment(EquipmentCard card, Dice dice)
    {
        card.ActivateEquipment(gameObject, _enemyController.gameObject, dice);
        return true;
    }

    public void StartTurn()
    {
        if (_healthComponent.CurrentPoison > 0)
            _healthComponent.TakeDamageByPoison(_healthComponent.CurrentPoison);

        foreach (var card in _equipmentPanel
                             .GetComponentsInChildren<EquipmentCard>())
            card.ResetUses();

        _dicePanel.ClearDice();
        for (int i = 0; i < _dicePanel.InitialCount; i++)
            _dicePanel.AddDice(_diceDefinition);
        SpawnEquipment();
    }
}
