using UnityEngine;

public abstract class AbstractHeatAction : ScriptableObject
{
    protected abstract int CalculateHeat();

    protected int CurrentHeat;
    public void ApplyEffect(GameObject enemy)
    {
        if (enemy.TryGetComponent<DicePanel>(out var dicePanel))
        {
            CurrentHeat = dicePanel.CurrentHeat;
            int heat = CalculateHeat();
            dicePanel.AddHeat(heat);
        }
        else
        {
        }
    }
}
