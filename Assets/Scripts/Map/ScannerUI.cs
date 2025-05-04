using UnityEngine;
using UnityEngine.UI;

public class ScannerUI : MonoBehaviour
{
    [SerializeField] private Button _scanButton;
    private PathogenController _player;
    private MapManager _mgr;

    void Start()
    {
        _player = FindObjectOfType<PathogenController>();
        _mgr = FindObjectOfType<MapManager>();

        if (_player == null)
            Debug.LogError("ScannerUI: не найден PathogenController на сцене!");

        if (_mgr == null)
            Debug.LogError("ScannerUI: не найден MapManager на сцене!");

        _scanButton.onClick.AddListener(OnScanClicked);
    }

    private void OnScanClicked()
    {
        _mgr.ScanNeighbors(_player.CurrentNode);
    }
}
