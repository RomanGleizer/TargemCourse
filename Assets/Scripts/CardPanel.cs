using System.Collections.Generic;
using UnityEngine;

public class CardPanel : MonoBehaviour
{
    [SerializeField] private EquipmentCard _equipmentCardPrefab;

    private Transform _equipmentPanel;

    private void Start()
    {
        _equipmentPanel = transform;
    }

    public void ClearCard()
    {
        foreach (Transform child in _equipmentPanel)
            Destroy(child.gameObject);
    }

    public EquipmentCard AddEquipment(EquipmentDefinition eqDef)
    {
        var card = Instantiate(_equipmentCardPrefab, _equipmentPanel);
        card.Initialize(eqDef);
        return card;
    }
}
