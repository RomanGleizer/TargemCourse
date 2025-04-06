using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 _startPosition;
    private Transform _parentBeforeDrag;
    private Transform _parentForDrag;
    private RectTransform _rectTransform;
    private Canvas _canvas;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _startPosition = transform.position;
        GameObject canvasObject = GameObject.Find("DragAndDropCanvas");
        _parentForDrag = canvasObject.transform;
        _canvas = canvasObject.GetComponent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _parentBeforeDrag = transform.parent;
        transform.SetParent(_parentForDrag);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //transform.position = Input.mousePosition;
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _startPosition;
        transform.SetParent(_parentBeforeDrag);
    }
}
