using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DropZone : MonoBehaviour, IDropHandler
{
    private EquipmentCard _card;
    private PathogenController _player;

    private void Start()
    {
        _card = GetComponent<EquipmentCard>();
        _player = FindObjectOfType<PathogenController>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;

        if (droppedObject.TryGetComponent(out Dice dice))
        {
            _player.TryActivateEquipment(_card, dice);           
            //Проверить возвращение кубика при невыполнении
            //Как вариант, сделать уничтожение кубика прямо здесь (равносильно)
        }
    }
}

