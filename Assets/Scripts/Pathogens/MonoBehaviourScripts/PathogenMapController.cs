using UnityEngine;

public class PathogenMapController : MonoBehaviour
{
    [SerializeField] private MapNode _startNode;

    public MapNode CurrentNode { get; private set; }

    private void Awake()
    {
        CurrentNode = _startNode;
        if (CurrentNode == null)
            Debug.LogError($"[{nameof(PathogenMapController)}] Start node не задан!");
        else
            transform.position = CurrentNode.transform.position;
    }

    public void MoveToNode(MapNode node)
    {
        CurrentNode = node;
        transform.position = node.transform.position;
    }
}
