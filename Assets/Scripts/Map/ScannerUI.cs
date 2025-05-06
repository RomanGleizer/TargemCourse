using UnityEngine;
using UnityEngine.UI;

public class ScannerUI : MonoBehaviour
{
    [SerializeField] private Button _scanButton;
    private PathogenMapController _player;
    private MapManager _mgr;

    private void OnEnable()
    {
        GameModeManager.Instance.OnModeChanged += HandleModeChanged;
    }

    private void OnDisable()
    {
        GameModeManager.Instance.OnModeChanged -= HandleModeChanged;
    }

    private void Start()
    {
        _player = FindObjectOfType<PathogenMapController>();
        _mgr = FindObjectOfType<MapManager>();

        _scanButton.onClick.AddListener(OnScanClicked);
        HandleModeChanged(GameModeManager.Instance.CurrentMode);
    }

    private void HandleModeChanged(GameModeManager.Mode mode)
    {
        _scanButton.gameObject.SetActive(mode == GameModeManager.Mode.Map);
    }

    private void OnScanClicked()
    {
        _mgr.ScanNeighbors(_player.CurrentNode);
    }
}
