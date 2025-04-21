using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Effects/Damage/DiceValueDamageEffect", fileName = "NewDiceValueDamageEffect")]
public class DiceValueDamageEffect : AbstractDamageAction
{
    protected override int CalculateDamage(int diceValue)
    {
        return diceValue;
    }
}
