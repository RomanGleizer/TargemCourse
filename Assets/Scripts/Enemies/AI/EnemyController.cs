using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PathogenController), typeof(DicePoolController))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private List<EquipmentCard> _equipmentCards;

    private DicePoolController _dicePool;
    private PathogenController _player;
    private HealthComponent _healthComponent;

    private void Awake()
    {
        _player = FindObjectOfType<PathogenController>();
        _healthComponent = GetComponent<HealthComponent>();
        _dicePool = GetComponent<DicePoolController>();
    }
    public void Start()
    {
        //_healthComponent.InitializeCurrentHealthOnStart(_definition.MaxHealth);
    }

    public void ActivateEquipment()
    {
        var availableDice = _dicePool.GetDicePool();

        foreach (var card in _equipmentCards)
        {
            var validDice = availableDice
                .Where(d => card.CanActivate(d.Value))
                .OrderBy(d => d.Value)
                .ToList();

            if (validDice.Count > 0)
            {
                var bestDice = validDice.Last();

                card.ActivateEquipment(gameObject, _player.gameObject, bestDice.Value);
                availableDice.Remove(bestDice);
            }
        }
    }
}
