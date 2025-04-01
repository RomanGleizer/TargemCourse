using UnityEngine;

public class NonCombatPathogenBehavior : IPathogenBehavior
{
    public void StartTurn()
    {
        Debug.Log("NonCombatBehavior: Ќачало небоевого хода (перемещение по карте).");
    }

    public void ExecuteAction(int diceValue)
    {
        Debug.Log("NonCombatBehavior: ¬ыполн€етс€ действие перемещени€ по карте.  убик (" + diceValue + ") не используетс€ дл€ боевых способностей.");
    }

    public void EndTurn()
    {
        Debug.Log("NonCombatBehavior:  онец небоевого хода патогена.");
    }
}
