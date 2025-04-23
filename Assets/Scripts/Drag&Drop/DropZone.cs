using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DropZone : MonoBehaviour, IDropHandler
{
    private EquipmentCard _card;

    private void Start()
    {
        _card = GetComponent<EquipmentCard>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;

        if (droppedObject.TryGetComponent(out Dice dice))
        {
            dice.InteractionWithCard(_card);
            Destroy(dice.gameObject);
        }
    }
}

