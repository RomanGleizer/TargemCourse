using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class PathogenController : MonoBehaviour
{
    [SerializeField] private PathogenDefinition _definition;
    [SerializeField] private Transform _equipmentPanel;
    [SerializeField] private EquipmentCard _equipmentCardPrefab;
    [SerializeField] private DicePanel _dicePanel;
    [SerializeField] private DiceDefinition _diceDefinition;

    private HealthComponent _healthComponent;
    private EnemyController _enemyController;

    public DicePanel DicePanel => _dicePanel;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _enemyController = FindObjectOfType<EnemyController>();
    }

    private void Start()
    {
        _healthComponent.InitializeHealth();
        SpawnEquipment();
    }

    private void SpawnEquipment()
    {
        foreach (var eqDef in _definition.EquipmentDefinitions)
        {
            var card = Instantiate(_equipmentCardPrefab, _equipmentPanel);
            card.Initialize(eqDef);
        }
    }

    public bool TryActivateEquipment(EquipmentCard card, Dice dice)
    {
        if (!card.CanActivate(dice.Value))
            return false;

        card.ActivateEquipment(gameObject, _enemyController.gameObject, dice.Value);
        Destroy(dice.gameObject);
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
    }
}
