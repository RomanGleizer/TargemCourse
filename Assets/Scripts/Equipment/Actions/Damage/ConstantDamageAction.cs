using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Effects/ConstantDamageEffect", fileName = "NewConstantDamageEffect")]
public class ConstantDamageEffect : AbstractDamageAction
{
    [SerializeField] private int damageAmount;

    protected override int CalculateDamage(int diceValue)
    {
        return damageAmount;
    }
}
