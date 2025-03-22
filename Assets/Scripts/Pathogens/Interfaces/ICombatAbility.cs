public interface ICombatAbility 
{
    /// <summary>
    /// Проверяет, соответствует ли выпавшее значение требованиям способности
    /// </summary>
    /// <param name="diceValue">Выпавшее значение кубика</param>
    /// <returns>True, если значение удовлетворяет условиям</returns>
    bool IsDiceValid(int diceValue);
}