using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private List<MapNode> _allNodes;
    private int _currentLayerIndex;

    private void Awake()
    {
        _allNodes = FindObjectsOfType<MapNode>().ToList();
        ShowOnlyLayer(0);
    }

    public void ShowOnlyLayer(int layerIndex)
    {
        _currentLayerIndex = layerIndex;
        foreach (var node in _allNodes)
        {
            var onThisLayer = node.LayerIndex == layerIndex;
            var sr = node.GetComponent<SpriteRenderer>();
            var c = sr.color;
            c.a = onThisLayer
                ? (node.IsScanned ? 1f : 0.2f)
                : 0f;
            sr.color = c;
        }
    }

    public void ScanNeighbors(MapNode current)
    {
        foreach (var nb in current.Neighbors)
            nb.Scan();

        if (current.IsTransitPoint && current.LinkedTransitNode != null)
            current.LinkedTransitNode.Scan();

        ShowOnlyLayer(_currentLayerIndex);
    }
}
