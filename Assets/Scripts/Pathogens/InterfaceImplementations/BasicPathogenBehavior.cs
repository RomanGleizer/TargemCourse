using UnityEngine;

public class BasicPathogenBehavior : IPathogenBehavior
{
    public void StartTurn()
    {
        Debug.Log("Start turn");
    }

    public void ExecuteAction(int diceValue)
    {
        /* Выполняем действие */
    }

    public void EndTurn()
    {
        Debug.Log("End turn");
    }
}