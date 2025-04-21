using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Effects/Damage/AdditionDiceValueDamageEffect", fileName = "NewAdditionDiceValueDamageEffect")]
public class AdditionDiceValueDamageEffect : AbstractDamageAction
{
    [SerializeField] private int damageAdditionAmount;
    protected override int CalculateDamage(int diceValue)
    {
        return diceValue + damageAdditionAmount;
    }
}
