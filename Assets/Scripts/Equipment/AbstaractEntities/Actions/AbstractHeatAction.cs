using UnityEngine;

public abstract class AbstractHeatAction : AbstractAction
{
    protected abstract int CalculateHeat();

    protected int CurrentHeat;
    public override void ApplyEffect(GameObject attacker, GameObject target, int diceValue)
    {
        if (target.TryGetComponent<DicePanel>(out var dicePanel))
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
