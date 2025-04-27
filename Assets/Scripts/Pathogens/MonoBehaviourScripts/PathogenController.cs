using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class PathogenController : MonoBehaviour
{
    [SerializeField] private PathogenDefinition definition;
    [SerializeField] private Transform equipmentContainer;
    [SerializeField] private EquipmentCard equipmentCardPrefab;

    private HealthComponent _healthComponent;
    private readonly List<EquipmentCard> _equipmentCards = new();

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
    }

    private void Start()
    {
        if (definition == null)
        {
            Debug.LogError("PathogenDefinition is null.");
            return;
        }

        _healthComponent.InitializeCurrentHealthOnStart(definition.MaxHealth);
    }

    public void TryActivateEquipment(EquipmentCard card, int diceValue)
    {
        if (card.CanActivate(diceValue))
        {
            card.ActivateEquipment(gameObject, diceValue);
        }
    }
}
