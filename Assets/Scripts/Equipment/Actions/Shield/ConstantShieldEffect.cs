using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Effects/Shield/ConstantShieldEffect", fileName = "NewConstantShieldEffect")]
public class ConstantShieldEffect : AbstractShieldAction
{
    [SerializeField] private int _shieldAmount;
    protected override int CalculateShield(int diceValue)
    {
        return _shieldAmount;
    }
}
