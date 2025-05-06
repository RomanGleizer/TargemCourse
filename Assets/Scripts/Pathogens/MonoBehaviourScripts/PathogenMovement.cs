using UnityEngine;

[RequireComponent(typeof(PathogenMapController))]
public class PathogenMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4f;

    private PathogenMapController _pathogen;
    private MapManager _mapManager;
    private bool _isMoving;
    private Vector3 _targetPos;

    private void Awake()
    {
        _pathogen = GetComponent<PathogenMapController>();
    }

    private void OnEnable()
    {
        if (GameModeManager.Instance != null)
            GameModeManager.Instance.OnModeChanged += HandleModeChanged;
    }

    private void OnDisable()
    {
        if (GameModeManager.Instance != null)
            GameModeManager.Instance.OnModeChanged -= HandleModeChanged;
    }

    private void Start()
    {
        var mode = GameModeManager.Instance != null
            ? GameModeManager.Instance.CurrentMode
            : GameModeManager.Mode.Map;
        HandleModeChanged(mode);

        _mapManager = FindObjectOfType<MapManager>();
        if (_mapManager == null)
        {
            Debug.LogError($"[{nameof(PathogenMovement)}] MapManager не найден в сцене", this);
            return;
        }

        var start = _pathogen.CurrentNode;
        if (start == null)
        {
            Debug.LogError($"[{nameof(PathogenMovement)}] CurrentNode не установлен в {nameof(PathogenMapController)}", this);
            return;
        }

        start.Scan();
        _mapManager.ShowOnlyLayer(start.LayerIndex);

        transform.position = start.transform.position;
    }

    private void HandleModeChanged(GameModeManager.Mode mode)
    {
        var active = mode == GameModeManager.Mode.Map;
        enabled = active;
        Debug.Log($"[PathogenMovement] mode → {mode}, component {(active ? "enabled" : "disabled")}");
    }

    private void Update()
    {
        if (!_isMoving) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            _targetPos,
            _moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, _targetPos) < 0.01f)
            _isMoving = false;
    }

    public void TryMoveTo(MapNode dest)
    {
        if (_isMoving) return;

        var current = _pathogen.CurrentNode;
        if (current == null) return;

        if (dest.LayerIndex == current.LayerIndex
            && current.Neighbors.Contains(dest)
            && dest.IsScanned)
        {
            BeginMove(dest);
            return;
        }
        if (current.IsTransitPoint
            && current.LinkedTransitNode == dest
            && dest.IsScanned)
        {
            BeginMove(dest);
            _mapManager.ShowOnlyLayer(dest.LayerIndex);
        }
    }

    private void BeginMove(MapNode dest)
    {
        _pathogen.MoveToNode(dest);

        _targetPos = dest.transform.position;
        _isMoving = true;
    }
}
