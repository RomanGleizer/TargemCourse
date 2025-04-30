using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent), typeof(DicePoolController))]
public class PathogenController : MonoBehaviour
{
    [SerializeField] private PathogenDefinition _definition;

    private DicePoolController _dicePool;
    private HealthComponent _healthComponent;
    private EnemyController _enemy;

    private void Awake()
    {
        _enemy = FindObjectOfType<EnemyController>();
        _healthComponent = GetComponent<HealthComponent>();
        _dicePool = GetComponent<DicePoolController>();
    }

    private void Start()
    {
        _healthComponent.InitializeCurrentHealthOnStart(_definition.MaxHealth);
        _dicePool.InitializePool();
    }

    public bool TryActivateEquipment(EquipmentCard card, Dice dice)
    {
        if (card.CanActivate(dice.Value))
        {
            card.ActivateEquipment(gameObject, _enemy.gameObject, dice.Value);
            Destroy(card.gameObject);
            Destroy(dice.gameObject);
            return true;
        }
        return false;
    }
}
