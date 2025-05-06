using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Button _continueButton;

    private List<MapNode> _allNodes;
    private int _currentLayerIndex;

    private void Awake()
    {
        GameModeManager.Instance?.SetMode(GameModeManager.Mode.Map);

        _allNodes = FindObjectsOfType<MapNode>().ToList();
        ShowOnlyLayer(0);

        if (_continueButton != null)
            _continueButton.onClick.AddListener(OnContinueClicked);
        else
            Debug.LogWarning($"[{nameof(MapManager)}] _continueButton не назначена в инспекторе", this);
    }

    private void OnContinueClicked()
    {
        SceneManager.LoadScene(1);
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
