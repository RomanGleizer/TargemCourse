using UnityEngine;

[RequireComponent(typeof(PathogenController))]
public class PathogenMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4f;

    private PathogenController _pathogen;
    private MapManager _mapManager;

    private bool _isMoving;
    private Vector3 _targetPos;

    private void Awake()
    {
        _pathogen = GetComponent<PathogenController>();
    }

    private void Start()
    {
        _mapManager = FindObjectOfType<MapManager>();

        var start = _pathogen.CurrentNode;
        transform.position = start.transform.position;

        start.Scan();
        _mapManager.ShowOnlyLayer(start.LayerIndex);
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
            return;
        }
    }

    private void BeginMove(MapNode dest)
    {
        _pathogen.MoveToNode(dest);

        _targetPos = dest.transform.position;
        _isMoving = true;
    }
}
