using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DropZone : MonoBehaviour, IDropHandler
{
    private Card _card;

    private void Start()
    {
        _card = GetComponent<Card>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;

        if (droppedObject.TryGetComponent(out EquipmentCard card))
        {
            card.ActivateEquipment(gameObject);
        }
    }
}

