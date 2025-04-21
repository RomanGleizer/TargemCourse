using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Effects/Damage/MultiplicationDiceValueDamageEffect", fileName = "NewMultiplicationDiceValueDamageEffect")]
public class MultiplicationDiceValueDamageEffect : AbstractDamageAction
{
    [SerializeField] private int damageMultiplicationAmount;

    protected override int CalculateDamage(int diceValue)
    {
        return diceValue * damageMultiplicationAmount;
    }
}
