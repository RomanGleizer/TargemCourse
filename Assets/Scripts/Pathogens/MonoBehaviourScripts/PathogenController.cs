using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthComponent))]
public class PathogenController : MonoBehaviour
{
    [Header("Battle Setup")]
    [SerializeField] private PathogenDefinition _definition;
    [SerializeField] private Transform _equipmentPanel;
    [SerializeField] private DicePanel _dicePanel;
    [SerializeField] private DiceDefinition _diceDefinition;
    [SerializeField] private CardPanel _cardPanel;
    [SerializeField] private Image _image;

    private HealthComponent _healthComponent;
    private EnemyController _enemyController;
    private bool _equipmentPopulatedThisTurn;

    private readonly Dictionary<PathogenDefinition, List<int>> _savedUses
        = new Dictionary<PathogenDefinition, List<int>>();

    public DicePanel DicePanel => _dicePanel;
    public CardPanel CardPanel => _cardPanel;
    public Image Image { get => _image; set => _image = value; }

    public PathogenDefinition Definition
    {
        get => _definition;
        set
        {
            if (_definition == value) return;
            _definition = value;
            _equipmentPopulatedThisTurn = false;
            EnsureSavedUses();
            PopulateEquipmentPanel();
        }
    }

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _enemyController = FindObjectOfType<EnemyController>();
        EnsureSavedUses();
    }

    private void OnEnable()
    {
        if (GameModeManager.Instance != null)
            GameModeManager.Instance.OnModeChanged += mode => enabled = mode == GameModeManager.Mode.Battle;
    }

    private void Start()
    {
        _healthComponent.InitializeHealth();
    }

    public void StartTurn()
    {
        _savedUses[_definition] = _definition
            .EquipmentDefinitions
            .Select(eq => eq.UsageCount)
            .ToList();

        _equipmentPopulatedThisTurn = false;
        PopulateEquipmentPanel();

        if (_healthComponent.CurrentPoison > 0)
            _healthComponent.TakeDamageByPoison(_healthComponent.CurrentPoison);

        foreach (var c in _equipmentPanel.GetComponentsInChildren<EquipmentCard>())
            c.ResetUses();

        _dicePanel.ClearDice();
        for (int i = 0; i < _dicePanel.InitialCount; i++)
            _dicePanel.AddDice(_diceDefinition);
    }

    public void ResetAllEquipment()
    {
        foreach (var def in _savedUses.Keys.ToList())
        {
            _savedUses[def] = def
                .EquipmentDefinitions
                .Select(eq => eq.UsageCount)
                .ToList();
        }

        _equipmentPopulatedThisTurn = false;
        PopulateEquipmentPanel();
    }

    private void PopulateEquipmentPanel()
    {
        bool inBattle = GameModeManager.Instance.CurrentMode == GameModeManager.Mode.Battle;
        if (inBattle && _equipmentPopulatedThisTurn) return;

        _equipmentPopulatedThisTurn = true;
        _cardPanel.ClearCard();

        var defs = _definition.EquipmentDefinitions;
        var uses = _savedUses[_definition];
        for (int i = 0; i < defs.Count; i++)
        {
            if (inBattle && uses[i] <= 0)
                continue;

            var card = _cardPanel.AddEquipment(defs[i]);
            card.SetRemainingUses(uses[i]);
        }
    }

    private void EnsureSavedUses()
    {
        if (_savedUses.ContainsKey(_definition)) return;
        _savedUses[_definition] = _definition
            .EquipmentDefinitions
            .Select(eq => eq.UsageCount)
            .ToList();
    }

    public bool TryActivateEquipment(EquipmentCard card, Dice dice)
    {
        if (!card.CanActivate(dice.Value))
            return false;

        bool used = card.ActivateEquipment(gameObject, _enemyController.gameObject, dice);
        if (used)
        {
            int idx = _definition.EquipmentDefinitions.IndexOf(card.Definition);
            if (idx >= 0)
                _savedUses[_definition][idx] = card.RemainingUses;
        }
        return used;
    }
}
