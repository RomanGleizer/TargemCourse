using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MapNode : MonoBehaviour
{
    [SerializeField] private int _layerIndex;
    [SerializeField] private bool _isTransitPoint;
    [SerializeField] private MapNode _linkedTransitNode;
    [SerializeField] private List<MapNode> _neighbors;

    private bool _isScanned;
    private SpriteRenderer _sr;

    public int LayerIndex => _layerIndex;
    public bool IsScanned => _isScanned;
    public bool IsTransitPoint => _isTransitPoint;
    public MapNode LinkedTransitNode => _linkedTransitNode;
    public List<MapNode> Neighbors => _neighbors;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        UpdateVisual();
    }

    public void Scan()
    {
        if (_isScanned) return;
        _isScanned = true;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        var c = _sr.color;
        c.a = _isScanned ? 1f : 0.2f;
        _sr.color = c;
    }
}
