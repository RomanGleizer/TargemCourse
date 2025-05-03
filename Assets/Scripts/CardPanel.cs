using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPanel : MonoBehaviour
{
    [SerializeField] private EquipmentCard _equipmentCardPrefab;

    private Transform _equipmentPanel;

    private void Start()
    {
        _equipmentPanel = this.transform;
    }

    public void ClearCard()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void AddEquipment(EquipmentDefinition eqDef)
    {
        var card = Instantiate(_equipmentCardPrefab, _equipmentPanel);
        card.Initialize(eqDef);
    }
}
