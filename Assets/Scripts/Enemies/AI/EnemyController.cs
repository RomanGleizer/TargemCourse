using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private DicePanel _dicePanel;
    [SerializeField] private DiceDefinition _diceDefinition;
    [SerializeField] private List<EquipmentDefinition> _equipmentDefinitions;

    private HealthComponent _healthComponent;
    private PathogenController _playerController;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _playerController = FindObjectOfType<PathogenController>();

        _healthComponent.InitializeHealth();
    }

    public void StartTurn()
    {
        if (_healthComponent.CurrentPoison > 0)
            _healthComponent.TakeDamageByPoison(_healthComponent.CurrentPoison);

        GenerateDice();
        ActivateEquipment();
    }

    private void GenerateDice()
    {
        _dicePanel.ClearDice();
        for (int i = 0; i < _dicePanel.InitialCount; i++)
            _dicePanel.AddDice(_diceDefinition);
    }

    public void ActivateEquipment()
    {
        var diceList = _dicePanel.GetDice();
        foreach (var eqDef in _equipmentDefinitions)
        {
            var valid = diceList
                .Where(d => eqDef.Condition == null
                            || eqDef.Condition.IsSatisfied(d.Value))
                .OrderBy(d => d.Value)
                .ToList();
            if (!valid.Any()) continue;

            var chosen = valid.Last();
            foreach (var effect in eqDef.Effects)
                effect.ApplyEffect(gameObject,
                                   _playerController.gameObject,
                                   chosen.Value);

            diceList.Remove(chosen);
            Destroy(chosen.gameObject);
        }
    }
}
