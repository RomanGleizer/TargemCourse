public interface IPathogenBehavior 
{
    /// <summary>
    /// Вызывается в начале хода патогена
    /// </summary>
    void StartTurn();

    /// <summary>
    /// Выполняет основное действие патогена, используя значение кубика
    /// </summary>
    /// <param name="diceValue">Выпавшее значение кубика</param>
    void ExecuteAction(int diceValue);

    /// <summary>
    /// Вызывается в конце хода для сброса состояний или подготовки к следующему раунду
    /// </summary>
    void EndTurn();
}