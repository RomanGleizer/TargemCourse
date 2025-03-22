public interface IPathogenMovement 
{
    /// <summary>
    /// Перемещает патоген в узел графа
    /// </summary>
    /// <param name="targetNode">Узел</param>
    void MoveTo(Node targetNode);

    /// <summary>
    /// Возвращает текущий узел, где находится патоген
    /// </summary>
    /// <returns>Текущий узел</returns>
    Node GetCurrentNode();
}