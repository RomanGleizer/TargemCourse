public interface IPathogenBehavior
{
    /// <summary>
    /// Вызывается в начале хода патогена.
    /// </summary>
    void StartTurn();

    /// <summary>
    /// Обрабатывает действие патогена с учетом значения кубика.
    /// В боевом поведении – проверяет кубик и активирует способность,
    /// в небоевом – может игнорировать кубик или выполнять логику перемещения.
    /// </summary>
    /// <param name="diceValue">Выпавшее значение кубика</param>
    void ExecuteAction(int diceValue);

    /// <summary>
    /// Завершает ход патогена.
    /// </summary>
    void EndTurn();
}
