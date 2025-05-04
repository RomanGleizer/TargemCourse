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
            Debug.LogError("ScannerUI: �� ������ PathogenController �� �����!");

        if (_mgr == null)
            Debug.LogError("ScannerUI: �� ������ MapManager �� �����!");

        _scanButton.onClick.AddListener(OnScanClicked);
    }

    private void OnScanClicked()
    {
        _mgr.ScanNeighbors(_player.CurrentNode);
    }
}
