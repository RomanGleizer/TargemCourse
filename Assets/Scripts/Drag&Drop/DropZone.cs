using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{ 
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;


        if (droppedObject.TryGetComponent(out Cube cube))
        {
            cube.ItsMe();
            print("This is card");
        }
    }
}

