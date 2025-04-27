using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 _startPosition;
    private Transform _parentBeforeDrag;
    private Transform _parentForDrag;
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private CanvasGroup _canvasGroup;

    void Start()
    {
        GameObject canvasObject = GameObject.Find("GameCanvas");

        _rectTransform = GetComponent<RectTransform>();       
        _canvas = canvasObject.GetComponent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();

        _startPosition = transform.position;        
        _parentForDrag = canvasObject.transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _parentBeforeDrag = transform.parent;
        transform.SetParent(_parentForDrag);
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _startPosition;
        transform.SetParent(_parentBeforeDrag);
        _canvasGroup.blocksRaycasts = true;
    }
}
